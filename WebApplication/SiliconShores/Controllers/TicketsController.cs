﻿using SiliconShores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Mail;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using Spire.Barcode;

namespace SiliconShores.Controllers
{
    public class TicketsController : Controller
    {
        private theme_park_dbEntities db = new theme_park_dbEntities();

         //GET: Tickets
        public ActionResult Index()
        {
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        public ActionResult ConfirmPurchase() 
        {
            ViewBag.TicketTypes = db.ticket_types.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPurchase(IDictionary<int, int> ticketPurchase, string email)
        {

            List<int> totalSales = new List<int>();
            List<ticket_sales> Sales = new List<ticket_sales>();
            List<int> ticketID = new List<int>();

            foreach (var ticket in ticketPurchase)
            {
                totalSales.AddRange(Enumerable.Repeat(ticket.Key, ticket.Value));
            }
            foreach (var s in totalSales)
            {
                db.ticket_sales.Add(db.CreateTicket(s));
                db.SaveChanges();
            }

            Sales = db.ticket_sales.OrderByDescending(t => t.ticket_id).Take(totalSales.Count()).ToList();
            foreach (ticket_sales sales in Sales)
            {
                ticketID.Add(sales.ticket_id);
            }

            GenerateBarCode(ticketID);
            GeneratePDF(ticketID.Count());
            MailMessage mail = new MailMessage("siliconshoressmtp@gmail.com", email)
            {
                Subject = "PURCHASE CONFIRMATION",
                Attachments = { new Attachment(@"C:\Users\Public\Documents\eTicketsForTicket.pdf") }
            };
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);


            return RedirectToAction("Index", "Home");
        }

        private void GenerateBarCode(List<int> ticketID)
        {
            BarcodeSettings setting = new BarcodeSettings();
            setting.Type = BarCodeType.UPCA;
            setting.Unit = GraphicsUnit.Millimeter;
            setting.X = 0.8F;

            int counter = 0;
            foreach (int ticket_id in ticketID)
            {
                counter++;
                string id = ticket_id.ToString();
                setting.Data = id;
                setting.Data2D = id;

                //Generate the barcode based
                BarCodeGenerator generator = new BarCodeGenerator(setting);
                Image barcode = generator.GenerateImage();

                //save the barcode as an image
                barcode.Save(@"C:\Users\Public\Documents\Ticket\BarCode"+counter+".png");
            }
        }

        private void GeneratePDF(int numberOfBarcodes)
        {
            //Create a pdf document 
            PdfDocument doc = new PdfDocument();

            //Createa one page
            PdfPageBase page = doc.Pages.Add();
            float pageWidth = page.Canvas.ClientSize.Width;
            float y = 0;

            //page header
            PdfPen pen1 = new PdfPen(Color.Gray, 2f);
            PdfBrush brush1 = new PdfSolidBrush(Color.Gray);
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 10f, FontStyle.Italic));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Right);
            String text = "Shilicon Shores Theme Park";
            page.Canvas.DrawString(text, font1, brush1, pageWidth, y, format1);
            SizeF size = font1.MeasureString(text, format1);
            y = y + size.Height + 2;
            page.Canvas.DrawLine(pen1, 0, y, pageWidth, y);

            //title
            y = y + 5;
            PdfBrush brush2 = new PdfSolidBrush(Color.Black);
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 32f, FontStyle.Bold));
            PdfStringFormat format2 = new PdfStringFormat(PdfTextAlignment.Center);
            format2.CharacterSpacing = 1f;
            text = "Thanks For Your Purchase";
            page.Canvas.DrawString(text, font2, brush2, pageWidth / 2, y, format2);
            size = font2.MeasureString(text, format2);
            y = y + size.Height + 10;
            text = "Your eTicket:";
            page.Canvas.DrawString(text, font2, brush2, pageWidth / 2, y, format2);
            size = font2.MeasureString(text, format2);
            y = y + size.Height + 10;

            for (int i = 1; i <= numberOfBarcodes; i++)
            {
                //icon
                PdfImage image = PdfImage.FromFile(@"C:\Users\Public\Documents\Ticket\BarCode"+i+".png");
                page.Canvas.DrawImage(image, new PointF(image.PhysicalDimension.Width / 2, y));
                float imageLeftSpace = pageWidth - image.PhysicalDimension.Width - 2;
                float imageBottom = image.PhysicalDimension.Height + y;
                y = y + image.PhysicalDimension.Height;
            }


            //refenrence content
            y = y + 10;
            PdfTrueTypeFont font3 = new PdfTrueTypeFont(new Font("Arial", 9f));
            PdfStringFormat format3 = new PdfStringFormat();
            format3.ParagraphIndent = font3.Size * 2;
            format3.MeasureTrailingSpaces = true;
            format3.LineSpacing = font3.Size * 1.5f;
            String text1 = "For any further questions or concerns, please contact ";
            String text2 = "SiliconShores.net";
            String text3 = ", ";
            String text4 = "Customer Severice at: University of Houston  ·  4800 Calhoun Rd. Houston, TX, 77004  ·  713-743-2255";
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
            format3.ParagraphIndent = font3.Size * 2;
            page.Canvas.DrawString(text4, font3, brush2, 0, y, format3);

            doc.SaveToFile(@"C:\Users\Public\Documents\eTicketsForTicket.pdf");
            doc.Close();

        }

    }
}