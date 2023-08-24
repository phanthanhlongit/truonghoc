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
        [Display(Name = "Tiêu đề")]
        public string title { get; set; }

        [StringLength(250)]
        [Display(Name = "Url")]
        public string slug { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        [Display(Name = "Nội dung")]
        public string content { get; set; }

        [Display(Name = "Tiêu đề Meta")]
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả Meta")]
        public string MetaDescription { get; set; }

        [Display(Name = "Từ khóa Meta")]
        public string MetaKeyword { get; set; }

        [AllowHtml]
        public string Script { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? DateUpdate { get; set; }

        [Display(Name = "Loại")]
        public int? Type { get; set; }

        [Display(Name = "Loại đặc biệt")]
        public bool IsSpecial { get; set; }

        [Display(Name = "Hiển thị trang chủ")]
        public bool IsShowHome { get; set; }

        [Display(Name = "Hiển thị hình ảnh bên trái")]
        public bool IsImgLeft { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? DateCreate { get; set; }

    }
}
