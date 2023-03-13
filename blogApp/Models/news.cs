namespace blogApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        public int id { get; set; }

        [StringLength(250)]
        [Display(Name ="News Title")]
        public string title { get; set; }

        [StringLength(250)]
        [Display(Name = "News Bref")]
        public string bref { get; set; }
        [Display(Name ="News Description")]
        public string desc { get; set; }

        [StringLength(250)]
        [Display(Name = "News Photo")]
        public string photo { get; set; }

        public DateTime? date { get; set; }
        [Display(Name = "Catigory")]
        public int? catId { get; set; }

        public int? userId { get; set; }

        public virtual catalog catalog { get; set; }

        public virtual user user { get; set; }
    }
}
