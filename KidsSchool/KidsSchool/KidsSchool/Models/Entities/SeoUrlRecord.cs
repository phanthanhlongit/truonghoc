namespace KidsSchool.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeoUrlRecord")]
    public partial class SeoUrlRecord
    {
        public long id { get; set; }

        [StringLength(250)]
        public string url { get; set; }

        [StringLength(250)]
        public string controller { get; set; }

        [StringLength(250)]
        public string action { get; set; }

        [StringLength(250)]
        public string objectId { get; set; }
    }
}
