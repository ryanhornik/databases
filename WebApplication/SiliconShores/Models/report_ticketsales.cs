//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiliconShores.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class report_ticketsales
    {
        public System.DateTime sale_date { get; set; }
        public Nullable<System.DateTime> redemption_date { get; set; }
        public string sale_location { get; set; }
        public int ticket_id { get; set; }
        public Nullable<float> ticket_price { get; set; }
        public System.DateTime weather_date { get; set; }
        public string weather_conditions { get; set; }
        public int high_temp { get; set; }
        public int low_temp { get; set; }
    }

    public class report_ticketsalesChartData
    {
        public string[] xValues;
        public int[] yValues;
    }
}
