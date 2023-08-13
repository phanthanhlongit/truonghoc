namespace KidsSchool.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HitCounter
    {
        public int id { get; set; }

        public int? SLID { get; set; }

        [StringLength(10)]
        public string IPAddress { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(10)]
        public string Device { get; set; }
    }
}
