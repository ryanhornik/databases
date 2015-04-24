using SiliconShores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Barcode;
using System.Drawing;
using System.Net.Mail;

namespace SiliconShores.Controllers
{
    public class PlanVacationController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

        // GET: PlanVacation
        public ActionResult Index()
        {
            ViewBag.RoomTypes = new SelectList(db.room_types, "room_type_id", "room_types_string");
            ViewBag.Hotels = new SelectList(db.hotels, "hotel_id", "hotel_name");
            ViewBag.Rooms = new SelectList(db.hotel_rooms, "hotel_and_room_type", "room_number");
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        public ActionResult ConfirmPurchase(DateTime arrivalDate, int nights, IDictionary<int, int> ticketPurchase, int Hotels, int RoomTypes, int Room) 
        {
            ViewBag.RoomTypes = db.room_types.ToList();
            ViewBag.Hotels = db.hotels.ToList();
            ViewBag.Rooms = db.hotel_rooms.ToList();
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPurchase(DateTime arrivalDate, int nights, IDictionary<int, int> ticketPurchase, int Hotels, int RoomTypes, int Room, bool? post, string email)
        {
            MailMessage mail = new MailMessage("siliconshoressmtp@gmail.com", email)
            {
            GenerateBarCode();
            GeneratePDF();
            MailMessage mail = new MailMessage("siliconshoressmtp@gmail.com", email)
            {
                Subject = "PURCHASE CONFIRMATION",
                Attachments = { new Attachment(@"C:\Users\Public\Documents\eTickets.pdf") }
            };
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);

            ticketPurchase = ticketPurchase.ToDictionary(s => s.Key, s => (nights+1)*s.Value);

            var selectedRoom = db.hotel_rooms.First(s => s.hotel_id == Hotels && s.room_number == Room);
            
            var hotelReservation = new hotel_reservations
            {
                hotel_rooms = selectedRoom,
                total_reservation_cost = selectedRoom.room_rate * nights,
                paid_in_full = false,
                reservation_checkin_date = arrivalDate,
                reservation_checkout_date = arrivalDate.AddDays(nights)
            };
            if (ModelState.IsValid)
            {
                db.hotel_reservations.Add(hotelReservation);
                db.SaveChanges();
            }

            List<int> totalSales = new List<int>();
            
            foreach (var ticket in ticketPurchase)
            {
                totalSales.AddRange(Enumerable.Repeat(ticket.Key,ticket.Value));
            }
            foreach (var s in totalSales)
            {
                db.ticket_sales.Add(db.CreateTicket(s));
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public JsonResult RoomSelection(string selectedHotel, string selectedRoomType)
        {

            if (selectedHotel.IsEmpty() || selectedRoomType.IsEmpty())
                return null;

            int sh = Convert.ToInt32(selectedHotel);
            int srt = Convert.ToInt32(selectedRoomType);

            var rooms = db.hotel_rooms.Where(s =>
                s.hotel_id == sh &&
                s.room_type_id == srt);

            return Json(rooms.Select(s => new
            {
                hotel_id = s.hotel_id,
                room_number = s.room_number,
                room_rate = s.room_rate,
                room_type_id = s.room_type_id
            }
                ), JsonRequestBehavior.AllowGet);
        }

        private void GenerateBarCode()
        {
            BarcodeSettings setting = new BarcodeSettings();

            setting.Data = "5463728191";
            setting.Data2D = "5463728191";

            setting.Type = BarCodeType.UPCA;
            setting.Unit = GraphicsUnit.Millimeter;

            setting.X = 0.8F;

            //Generate the barcode based
            BarCodeGenerator generator = new BarCodeGenerator(setting);

            Image barcode = generator.GenerateImage();

            //save the barcode as an image
            barcode.Save(@"C:\Users\Public\Documents\BarCode.png");

        }

        private void GeneratePDF()
        {
            //Create a pdf document 
            PdfDocument doc = new PdfDocument();

            //Createa one page
            PdfPageBase page = doc.Pages.Add();
            float pageWidth = page.Canvas.ClientSize.Width;
            float y = 0;

            //page header
            PdfPen pen1 = new PdfPen(Color.LightGray, 1f);
            PdfBrush brush1 = new PdfSolidBrush(Color.LightGray);
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 8f, FontStyle.Italic));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Right);
            String text = "Shilicon Shores Theme Park";
            page.Canvas.DrawString(text, font1, brush1, pageWidth, y, format1);
            SizeF size = font1.MeasureString(text, format1);
            y = y + size.Height + 1;
            page.Canvas.DrawLine(pen1, 0, y, pageWidth, y);

            //title
            y = y + 5;
            PdfBrush brush2 = new PdfSolidBrush(Color.Black);
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 24f, FontStyle.Bold));
            PdfStringFormat format2 = new PdfStringFormat(PdfTextAlignment.Center);
            format2.CharacterSpacing = 1f;
            text = "Thanks For Your Purchase";
            page.Canvas.DrawString(text, font2, brush2, pageWidth / 2, y, format2);
            size = font2.MeasureString(text, format2);
            y = y + size.Height + 6;

            //icon
            PdfImage image = PdfImage.FromFile(@"C:\Users\Public\Documents\BarCode.png");
            page.Canvas.DrawImage(image, new PointF(pageWidth - image.PhysicalDimension.Width, y));
            float imageLeftSpace = pageWidth - image.PhysicalDimension.Width - 2;
            float imageBottom = image.PhysicalDimension.Height + y;


            //refenrence content
            PdfTrueTypeFont font3 = new PdfTrueTypeFont(new Font("Arial", 9f));
            PdfStringFormat format3 = new PdfStringFormat();
            format3.ParagraphIndent = font3.Size * 2;
            format3.MeasureTrailingSpaces = true;
            format3.LineSpacing = font3.Size * 1.5f;
            String text1 = "(All text and picture from ";
            String text2 = "Wikipedia";
            String text3 = ", the free encyclopedia)";
            page.Canvas.DrawString(text1, font3, brush2, 0, y, format3);

            size = font3.MeasureString(text1, format3);
            float x1 = size.Width;
            format3.ParagraphIndent = 0;
            PdfTrueTypeFont font4 = new PdfTrueTypeFont(new Font("Arial", 9f, FontStyle.Underline));
            PdfBrush brush3 = PdfBrushes.Blue;
            page.Canvas.DrawString(text2, font4, brush3, x1, y, format3);
            size = font4.MeasureString(text2, format3);
            x1 = x1 + size.Width;

            page.Canvas.DrawString(text3, font3, brush2, x1, y, format3);
            y = y + size.Height;

            //content
            PdfStringFormat format4 = new PdfStringFormat();
            text = "Somestuff";
            PdfTrueTypeFont font5 = new PdfTrueTypeFont(new Font("Arial", 10f));
            format4.LineSpacing = font5.Size * 1.5f;
            PdfStringLayouter textLayouter = new PdfStringLayouter();
            float imageLeftBlockHeight = imageBottom - y;
            PdfStringLayoutResult result
                = textLayouter.Layout(text, font5, format4, new SizeF(imageLeftSpace, imageLeftBlockHeight));
            if (result.ActualSize.Height < imageBottom - y)
            {
                imageLeftBlockHeight = imageLeftBlockHeight + result.LineHeight;
                result = textLayouter.Layout(text, font5, format4, new SizeF(imageLeftSpace, imageLeftBlockHeight));
            }
            foreach (LineInfo line in result.Lines)
            {
                page.Canvas.DrawString(line.Text, font5, brush2, 0, y, format4);
                y = y + result.LineHeight;
            }

            doc.SaveToFile(@"C:\Users\Public\Documents\eTickets.pdf");
            doc.Close();

        }
    }
}