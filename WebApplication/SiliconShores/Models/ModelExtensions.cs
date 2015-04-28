using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.Ajax.Utilities;
using SiliconShores.Controllers;
using Spire.Barcode;
using Image = System.Drawing.Image;

namespace SiliconShores.Models
{
    public partial class theme_park_dbEntities
    {
        public ticket_sales CreateTicket(int type)
        {
            var sale = this.ticket_sales.Create();
            sale.sale_date = DateTime.Today;
            sale.ticket_type_id = type;
            sale.redemption_date = null;
            sale.sale_location = "Online";
            sale.theme_park = this.theme_park.First(s => s.theme_park_name.Equals("Silicon Shores"));
            return sale;
        }

        public void sendLastTickets(string email, int numTickets, string bodyText)
        {
            var allTickets = ticket_sales
                .Include("ticket_types")
                .OrderByDescending(t => t.ticket_id)
                .Take(numTickets).ToList();
            var pdfs = new List<MemoryStream>();
            foreach (var ticket in allTickets)
            {
                pdfs.Add(ticket.GeneratePDF());
            }
            var pdfFinal = Helper.MergePdf(pdfs);

            MailMessage mail = new MailMessage("siliconshoressmtp@gmail.com", email)
            {
                Subject = "Your Silicon Shores Purchase",
                Body = bodyText
            };
            mail.Attachments.Add(new Attachment(pdfFinal, "Silicon Shores Tickets.pdf"));

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Send(mail);
        }
    }

    public class attractionMetadata
    {
        [Required]
        [Display(Name = "Attraction")]
        public int attractions_id { get; set; }
        [Required]
        [Display(Name = "Attraction Name")]
        public string attraction_name { get; set; }
        [Required]
        [Display(Name = "Theme Area")]
        public int theme_area_id { get; set; }
        [Required]
        [Display(Name = "Attraction Description")]
        public string attraction_description { get; set; }
        [Required]
        [Display(Name = "Picture Path")]
        public string picture_path { get; set; }
        [Required]
        [Display(Name = "Is Working?")]
        public bool is_working { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Opened")]
        public System.DateTime date_opened { get; set; }
        [Required]
        [Display(Name = "Breakdowns")]
        public virtual ICollection<breakdown> breakdowns { get; set; }
        [Required]
        [Display(Name = "Daily Ride Report")]
        public virtual ICollection<daily_ride_report> daily_ride_report { get; set; }
        [Required]
        [Display(Name = "Theme Areas")]
        public virtual theme_areas theme_areas { get; set; }
    }
    [MetadataType(typeof(attractionMetadata))]
    public partial class attraction
    {
        public String getImagePath()
        {
            return "/Content/Images" + theme_areas.theme_area_pictures + picture_path;
        }
    }

    public class breakdownMetadata
    {
        [Required]
        [Display(Name = "Breakdown ID")]
        public int breakdown_id { get; set; }
        [Required]
        [Display(Name = "Attraction")]
        public int attraction_id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Incidence Date")]
        public System.DateTime incidence_date { get; set; }
        [Required]
        [Display(Name = "Resolution Date")]
        public Nullable<System.DateTime> resolution_date { get; set; }
        [Required]
        [Display(Name = "Repair Cost")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> repair_cost { get; set; }
        [Required]
        [Display(Name = "Attraction")]
        public virtual attraction attraction { get; set; }
    }
    [MetadataType(typeof(breakdownMetadata))]
    public partial class breakdown
    {
        public string fullSearchString()
        {
            return incidence_date.ToLongDateString() + " " + incidence_date.ToShortDateString() + " " +
                   ((!resolution_date.HasValue)
                       ? "Unresolved Broken"
                       : "Fixed" + resolution_date.Value.ToLongDateString() + " " +
                         resolution_date.Value.ToShortDateString()) + " " +
                   ((!repair_cost.HasValue) ? "" : repair_cost.Value.ToString("C")) + " " +
                   attraction.attraction_name;
        }
    }

    public class daily_ride_reportMetadata
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ride Report Date")]
        public System.DateTime ride_report_date { get; set; }
        [Required]
        [Display(Name = "Attraction")]
        public int attraction_id { get; set; }
        [Required]
        [Display(Name = "Total Riders")]
        public int total_riders { get; set; }
        [Required]
        [Display(Name = "Attraction")]
        public virtual attraction attraction { get; set; }
    }
    [MetadataType(typeof(daily_ride_reportMetadata))]
    public partial class daily_ride_report
    {
        public string fullSearchString()
        {
            return ride_report_date.ToLongDateString() + " " + ride_report_date.ToShortDateString() +
                   attraction.attraction_name + " " + total_riders;
        }
    }

    public class daily_weatherMetadata
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Weather Date")]
        public System.DateTime weather_date { get; set; }
        [Required]
        [Display(Name = "Theme Park")]
        public int theme_park_id { get; set; }
        [Required]
        [Display(Name = "Weather Conditions")]
        public string weather_conditions { get; set; }
        [Required]
        [Display(Name = "High Temperture")]
        public int high_temp { get; set; }
        [Required]
        [Display(Name = "Low Temperture")]
        public int low_temp { get; set; }
        [Required]
        [Display(Name = "Theme Park")]
        public virtual theme_park theme_park { get; set; }
    }
    [MetadataType(typeof(daily_weatherMetadata))]
    public partial class daily_weather
    {
        public string fullSearchString()
        {
            return weather_date.ToLongDateString() + " " + weather_date.ToShortDateString() + " " +
                   weather_conditions + " " + high_temp + " " + low_temp;
        }
    }

    public class employeeMetadata
    {
        [Required]
        [Range(100000000, 999999999)]
        [Display(Name = "SSN")]
        public int ssn { get; set; }
        [Required]
        [Display(Name = "Theme Park")]
        public int theme_park_id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Display(Name = "Middle Initial")]
        public string middle_initial { get; set; }
        [Required]
        [Display(Name = "Full Time")]
        public bool full_time { get; set; }
        [Required]
        [Display(Name = "Pay Rate")]
        [DataType(DataType.Currency)]
        public decimal payrate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hired Date")]
        public System.DateTime hired_date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Left")]
        public Nullable<System.DateTime> date_left { get; set; }
        [Display(Name = "Rehireable")]
        public Nullable<bool> rehireable { get; set; }
        [Display(Name = "Employee ID")]
        public string employee_id { get; set; }
        [Required]
        [Display(Name = "Theme Park")]
        public virtual theme_park theme_park { get; set; }
        [Display(Name = "User")]
        public virtual user user { get; set; }
    }
    [MetadataType(typeof(employeeMetadata))]
    public partial class employee
    {
        public string fullSearchString()
        {
            return ssn + " " +
                   first_name + " " + middle_initial + " " + last_name + " " +
                   (full_time ? "full time" : "part time") + " " +
                   ((!rehireable.HasValue)
                       ? "Current Employee"
                       : (rehireable.Value) ? "Quit Rehireable" : "Fired Not Rehireable") + " " +
                   payrate + " " +
                   hired_date.ToLongDateString() + hired_date.ToShortDateString() + " " +
                   ((!date_left.HasValue)
                       ? ""
                       : date_left.Value.ToLongDateString() + date_left.Value.ToShortDateString()) + " " +
                       ((user != null) ? user.Email : "");
        }
    }

    public class food_categoriesMetadata
    {
        [Required]
        [Display(Name = "Food Category")]
        public int food_category_id { get; set; }
        [Required]
        [Display(Name = "Food Categories Name")]
        public string food_categories_name { get; set; }
        [Required]
        [Display(Name = "Restaurants")]
        public virtual ICollection<restaurant> restaurants { get; set; }
    }
    [MetadataType(typeof(food_categoriesMetadata))]
    public partial class food_categories
    {
    }

    public class hotelMetadata
    {
        [Required]
        [Display(Name = "Hotel")]
        public int hotel_id { get; set; }
        [Required]
        [Display(Name = "Hotel Name")]
        public string hotel_name { get; set; }
        [Required]
        [Display(Name = "Pets Allowed")]
        public bool pets_allowed { get; set; }
        [Required]
        [Display(Name = "Theme Area")]
        public int theme_area_id { get; set; }
        [Required]
        [Display(Name = "Hotel Rooms")]
        public virtual ICollection<hotel_rooms> hotel_rooms { get; set; }
        [Required]
        [Display(Name = "Theme Areas")]
        public virtual theme_areas theme_areas { get; set; }
    }
    [MetadataType(typeof(hotelMetadata))]
    public partial class hotel
    {
    }

    public class hotel_reservationsMetadata
    {
        [Required]
        [Display(Name = "Reservation ID")]
        public int reservation_id { get; set; }
        [Required]
        [Display(Name = "Hotel")]
        public int hotel_id { get; set; }
        [Required]
        [Display(Name = "Room Number")]
        public int room_number { get; set; }
        [Required]
        [Display(Name = "Reservation Check-In Date")]
        [DataType(DataType.Date)]
        public System.DateTime reservation_checkin_date { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Reservation Check-Out Date")]
        public System.DateTime reservation_checkout_date { get; set; }
        [Required]
        [Display(Name = "Total Reservation Cost")]
        [DataType(DataType.Currency)]
        public decimal total_reservation_cost { get; set; }
        [Required]
        [Display(Name = "Paid In Full")]
        public bool paid_in_full { get; set; }
        [Required]
        [Display(Name = "Hotel Rooms")]
        public virtual hotel_rooms hotel_rooms { get; set; }
    }
    [MetadataType(typeof(hotel_reservationsMetadata))]
    public partial class hotel_reservations
    {
        public string fullSearchString()
        {
            return reservation_id + " " +
                   hotel_rooms.hotel.hotel_name + " " + room_number + " " +
                   total_reservation_cost + " " +
                   reservation_checkin_date.ToLongDateString() + " " + reservation_checkin_date.ToShortDateString() +
                   " " +
                   reservation_checkout_date.ToLongDateString() + " " + reservation_checkout_date.ToShortDateString() +
                   " " +
                   ((paid_in_full)
                    ? "Fully Paid in full"
                    : "Unpaid");
        }
    }

    public class hotel_roomsMetadata
    {
        [Required]
        [Display(Name = "Hotel")]
        public int hotel_id { get; set; }
        [Required]
        [Display(Name = "Room Number")]
        public int room_number { get; set; }
        [Required]
        [Display(Name = "Room Type")]
        public int room_type_id { get; set; }
        [Required]
        [Display(Name = "Room Rate")]
        [DataType(DataType.Currency)]
        public decimal room_rate { get; set; }
        [Required]
        [Display(Name = "Occupied")]
        public bool occupied { get; set; }
        [Required]
        [Display(Name = "Hotel Reservations")]
        public virtual ICollection<hotel_reservations> hotel_reservations { get; set; }
        [Required]
        [Display(Name = "Hotel")]
        public virtual hotel hotel { get; set; }
        [Required]
        [Display(Name = "Room Types")]
        public virtual room_types room_types { get; set; }
    }
    [MetadataType(typeof(hotel_roomsMetadata))]
    public partial class hotel_rooms
    {
        public string fullSearchString()
        {
            return hotel.hotel_name + " " + room_number + " " +
                   room_rate + " " + room_types.room_types_string;
        }
    }

    public class restaurantMetadata
    {
        [Required]
        [Display(Name = "Restaurant")]
        public int restaurant_id { get; set; }
        [Required]
        [Display(Name = "Restaurant Name")]
        public string restaurant_name { get; set; }
        [Required]
        [Display(Name = "Food Category")]
        public int food_category_id { get; set; }
        [Required]
        [Display(Name = "Theme Area")]
        public int theme_area_id { get; set; }

        [Required]
        [Display(Name = "Restaurant Daily Reports")]
        public virtual ICollection<restaurant_daily_reports> restaurant_daily_reports { get; set; }
        [Required]
        [Display(Name = "Theme Areas")]
        public virtual theme_areas theme_areas { get; set; }
        [Required]
        [Display(Name = "Food Categories")]
        public virtual food_categories food_categories { get; set; }
    }
    [MetadataType(typeof(restaurantMetadata))]
    public partial class restaurant
    {
    }

    public class restaurant_daily_reportsMetadata
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Report Date")]
        public System.DateTime report_date { get; set; }
        [Required]
        [Display(Name = "Restaurant")]
        public int restaurant_id { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Gross Income")]
        public decimal gross_income { get; set; }
        [Required]
        [Display(Name = "Patrons Served")]
        public int patrons_served { get; set; }

        [Required]
        [Display(Name = "Restaurant")]
        public virtual restaurant restaurant { get; set; }
    }
    [MetadataType(typeof(restaurant_daily_reportsMetadata))]
    public partial class restaurant_daily_reports
    {
        public string fullSearchString()
        {
            return report_date.ToLongDateString() + " " + report_date.ToShortDateString() + " " +
                   restaurant.restaurant_name + " " + gross_income.ToString("C") + " " + patrons_served;
        }
    }

    public class roleMetadata
    {
        [Required]
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Users")]
        public virtual ICollection<user> users { get; set; }
    }
    [MetadataType(typeof(roleMetadata))]
    public partial class role
    {
    }

    public class room_typesMetadata
    {
        [Required]
        [Display(Name = "Room Type")]
        public int room_type_id { get; set; }
        [Required]
        [Display(Name = "Room Type")]
        public string room_types_string { get; set; }

        [Required]
        [Display(Name = "Hotel Rooms")]
        public virtual ICollection<hotel_rooms> hotel_rooms { get; set; }
    }
    [MetadataType(typeof(room_typesMetadata))]
    public partial class room_types
    {
    }

    public class theme_areasMetadata
    {
        [Required]
        [Display(Name = "Theme Area")]
        public int theme_area_id { get; set; }
        [Required]
        [Display(Name = "Theme Area Name")]
        public string theme_area_name { get; set; }
        [Required]
        [Display(Name = "Theme Area Description")]
        public string theme_area_description { get; set; }
        [Required]
        [Display(Name = "Theme Area Pictures")]
        public string theme_area_pictures { get; set; }
        [Required]
        [Display(Name = "Theme Park")]
        public int theme_park_id { get; set; }
        [Required]
        [Display(Name = "Attractions")]
        public virtual ICollection<attraction> attractions { get; set; }
        [Required]
        [Display(Name = "Hotels")]
        public virtual ICollection<hotel> hotels { get; set; }
        [Required]
        [Display(Name = "Restaurants")]
        public virtual ICollection<restaurant> restaurants { get; set; }
        [Required]
        [Display(Name = "Theme Park")]
        public virtual theme_park theme_park { get; set; }
    }
    [MetadataType(typeof(theme_areasMetadata))]
    public partial class theme_areas
    {
        public List<String> getImagePaths()
        {
            List<String> images = new List<String>();
            foreach (attraction attr in attractions)
            {
                images.Add(attr.getImagePath());
            }
            return images;
        }
    }

    public class theme_parkMetadata
    {
        [Required]
        [Display(Name = "Theme Park")]
        public int theme_park_id { get; set; }
        [Required]
        [Display(Name = "Theme Park Name")]
        public string theme_park_name { get; set; }
        [Required]
        [Display(Name = "Park Open")]
        public System.TimeSpan park_open { get; set; }
        [Required]
        [Display(Name = "Park Close")]
        public System.TimeSpan park_close { get; set; }
        [Required]
        [Display(Name = "Park Country")]
        public string park_country { get; set; }
        [Required]
        [Display(Name = "Park State or Province")]
        public string park_state_or_province { get; set; }
        [Required]
        [Display(Name = "Park City")]
        public string park_city { get; set; }
        [Required]
        [Display(Name = "Park Street Address")]
        public string park_street_address { get; set; }

        [Required]
        [Display(Name = "Daily Weather")]
        public virtual ICollection<daily_weather> daily_weather { get; set; }
        [Required]
        [Display(Name = "Employees")]
        public virtual ICollection<employee> employees { get; set; }
        [Required]
        [Display(Name = "Theme Areas")]
        public virtual ICollection<theme_areas> theme_areas { get; set; }
        [Required]
        [Display(Name = "Ticket Sales")]
        public virtual ICollection<ticket_sales> ticket_sales { get; set; }
    }
    [MetadataType(typeof(theme_parkMetadata))]
    public partial class theme_park
    {
    }

    public class ticket_salesMetadata
    {
        [Required]
        [Display(Name = "Ticket ID")]
        public int ticket_id { get; set; }
        [Required]
        [Display(Name = "Ticket Type")]
        public int ticket_type_id { get; set; }
        [Required]
        [Display(Name = "Sale Date")]
        [DataType(DataType.Date)]
        public System.DateTime sale_date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Redemption Date")]
        public Nullable<System.DateTime> redemption_date { get; set; }
        [Required]
        [Display(Name = "Theme Park")]
        public int theme_park_id { get; set; }
        [Required]
        [Display(Name = "Sale Location")]
        public string sale_location { get; set; }
    }
    [MetadataType(typeof(ticket_salesMetadata))]
    public partial class ticket_sales
    {
        public MemoryStream GeneratePDF()
        {
            var doc = new Document();
            var ms = new MemoryStream();

            var writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();

            PdfPTable table = new PdfPTable(1);
            PdfPCell cell = new PdfPCell();

            string htmlBeforeImage =
                "<h1 class=\"text-center\">Silicon Shores</h1>\n" +
                "<h3 class=\"col-md-4\">One " + ticket_types.ticket_name + " Ticket</h3>";

            string htmlAfterImage =
                "<p>Thank you for your ticket purchase! This ticket is redeemable any time at Silicon Shores Theme Park for a days admission</p>\n" +
                "<p>Requirements for redemption: " + ticket_types.ticket_restrictions + ".</p>\n" +
                File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Views/TicketPdf.cshtml"));

            var bootstrap = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/bootstrap.min.css"))
                          + File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/bootstrap-theme.min.css"));

            ElementList list = XMLWorkerHelper.ParseToElementList(htmlBeforeImage, bootstrap);
            foreach (var element in list)
            {
                cell.AddElement(element);
            }

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(BarCodeImage(), BaseColor.WHITE);
            var scale = 115;
            img.ScalePercent(scale);
            cell.AddElement(img);

            list = XMLWorkerHelper.ParseToElementList(htmlAfterImage, bootstrap);
            foreach (var element in list)
            {
                cell.AddElement(element);
            }

            table.AddCell(cell);

            doc.Add(table);

            writer.CloseStream = false;
            doc.Close();
            ms.Position = 0;

            return ms;

        }

        private System.Drawing.Image BarCodeImage()
        {
            BarcodeSettings setting = new BarcodeSettings
            {
                Type = BarCodeType.Code39,
                Unit = GraphicsUnit.Millimeter,
                X = 0.4F,
                Data = ticket_id.ToString(),
                ShowText = false
            };


            BarCodeGenerator generator = new BarCodeGenerator(setting);
            return generator.GenerateImage();
        }

        public string fullSearchString()
        {
            return ticket_id + " " +
                   ticket_types.ticket_name + " " +
                   sale_date.ToLongDateString() + " " +
                   sale_date.ToShortDateString() + " " +
                   (redemption_date.IfNotNull(s => s.Value.ToLongDateString() + " " + s.Value.ToShortDateString())) + " " +
                   sale_location;
        }
    }

    public class ticket_typesMetadata
    {
        [Required]
        [Display(Name = "Ticket Type")]
        public int ticket_type_id { get; set; }
        [Required]
        [Display(Name = "Ticket Name")]
        public string ticket_name { get; set; }
        [Required]
        [Display(Name = "Ticket Restrictions")]
        public string ticket_restrictions { get; set; }
        [Required]
        [Display(Name = "Ticket Price")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> ticket_price { get; set; }
        [Required]
        [Display(Name = "Ticket Sales")]
        public virtual ICollection<ticket_sales> ticket_sales { get; set; }
    }
    [MetadataType(typeof(ticket_typesMetadata))]
    public partial class ticket_types
    {
    }
}