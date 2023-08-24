namespace KidsSchool.Models.DB
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Banner
    {
        public int BannerId { get; set; }

        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }

        [Display(Name = "Loại banner")]
        public int BannerTypeId { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Images { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool Active { get; set; }

        [Display(Name = "Vị trí banner")]
        public int BannerPositionId { get; set; }

        [Display(Name = "Mục tiêu")]
        public int ItemFor { get; set; }

        [Display(Name = "Mục tiêu ID")]
        public int ItemForId { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? DateUpdate { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? DateCreate { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }

        public virtual BannerPosition BannerPosition { get; set; }

        public virtual BannerType BannerType { get; set; }
    }

}
