
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
    
public partial class job_titles
{

    public job_titles()
    {

        this.employees = new HashSet<employee>();

    }


    public int job_title_id { get; set; }

    public string job_title { get; set; }



    public virtual ICollection<employee> employees { get; set; }

}

}
