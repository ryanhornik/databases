
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
    
public partial class employee
{

    public int ssn { get; set; }

    public int theme_park_id { get; set; }

    public string first_name { get; set; }

    public string last_name { get; set; }

    public string middle_initial { get; set; }

    public bool full_time { get; set; }

    public decimal payrate { get; set; }

    public System.DateTime hired_date { get; set; }

    public int job_title_id { get; set; }

    public Nullable<System.DateTime> date_left { get; set; }

    public Nullable<bool> rehireable { get; set; }

    public string username { get; set; }

    public string password { get; set; }



    public virtual job_titles job_titles { get; set; }

    public virtual theme_park theme_park { get; set; }

}

}
