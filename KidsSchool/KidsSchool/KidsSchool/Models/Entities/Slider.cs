namespace KidsSchool.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slider
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        [StringLength(250)]
        public string Caption { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        public int Target { get; set; }

        public DateTime? DateUpdate { get; set; }

        public DateTime? DateCreate { get; set; }
    }
}
