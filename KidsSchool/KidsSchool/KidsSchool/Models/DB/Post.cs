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

        [Display(Name = "Mã danh mục")]
        public int? CatId { get; set; }

        [Display(Name = "Tiêu đề")]
        [StringLength(250)]
        public string Title { get; set; }

        [Display(Name = "Đường dẫn")]
        [StringLength(250)]
        public string Slug { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Display(Name = "Biểu tượng")]
        [StringLength(250)]
        public string Icon { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? createDate { get; set; }

        [Display(Name = "Người dùng")]
        [StringLength(128)]
        public string UserId { get; set; }

        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }

        [Display(Name = "Tiêu đề Meta")]
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả Meta")]
        public string MetaDescription { get; set; }

        [Display(Name = "Từ khóa Meta")]
        public string MetaKeyword { get; set; }

        [Display(Name = "Mã script")]
        [AllowHtml]
        public string Script { get; set; }

        [Display(Name = "Ngày đăng tự động")]
        public DateTime? AutoPostDate { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? DateUpdate { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? DateCreate { get; set; }

        [Display(Name = "Lượt xem")]
        public int Views { get; set; }

        [Display(Name = "Đặc biệt")]
        public bool IsSpecial { get; set; }

        public virtual Category Category { get; set; }
    }
}
