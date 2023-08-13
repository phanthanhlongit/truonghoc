namespace KidsSchool.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page
    {
        public int id { get; set; }

        [StringLength(250)]
        public string title { get; set; }

        [StringLength(250)]
        public string slug { get; set; }

        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        public string content { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public string Script { get; set; }

        public DateTime? DateUpdate { get; set; }

        public int? Type { get; set; }

        public DateTime? DateCreate { get; set; }
    }
}
