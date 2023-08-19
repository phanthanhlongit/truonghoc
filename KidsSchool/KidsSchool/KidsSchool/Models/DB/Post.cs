namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Post
    {
        public int Id { get; set; }

        public int? CatId { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Slug { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        public string Content { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public bool Active { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        [AllowHtml]
        public string Script { get; set; }

        public DateTime? AutoPostDate { get; set; }

        public DateTime? DateUpdate { get; set; }

        public DateTime? DateCreate { get; set; }

        public int Views { get; set; }

        public bool IsSpecial { get; set; }

        public virtual Category Category { get; set; }
    }
}
