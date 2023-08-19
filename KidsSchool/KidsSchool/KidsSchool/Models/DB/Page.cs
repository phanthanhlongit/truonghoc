namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Page
    {
        public int id { get; set; }

        [StringLength(250)]
        public string title { get; set; }

        [StringLength(250)]
        public string slug { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        public string content { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        [AllowHtml]
        public string Script { get; set; }

        public DateTime? DateUpdate { get; set; }

        public int? Type { get; set; }

        public bool IsSpecial { get; set; }
        public bool IsShowHome { get; set; }
        public bool IsImgLeft { get; set; }

        public DateTime? DateCreate { get; set; }
    }
}
