namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Config")]
    public partial class Config
    {
        public string Icon { get; set; }

        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Mail { get; set; }

        [Display(Name = "Tiêu đề Meta")]
        public string MetaTitle { get; set; }

        [Display(Name = "Bản quyền")]
        public string Copyright { get; set; }

        [Display(Name = "URL Bản đồ")]
        public string UrlMap { get; set; }

        [Display(Name = "ID Thể loại")]
        public int? CatId { get; set; }

        [Display(Name = "Mô tả Meta")]
        public string MetaDescription { get; set; }

        [Display(Name = "Từ khóa Meta")]
        public string MetaKeyword { get; set; }

        [Display(Name = "Kịch bản trang")]
        public string ScriptAllPage { get; set; }

        [Display(Name = "Kịch bản cuối trang (Footer)")]
        public string IndexScriptFooter { get; set; }

        [Display(Name = "Kịch bản đầu trang (Header)")]
        public string IndexScriptHeader { get; set; }

        [Display(Name = "Ngày Cập nhật")]
        public DateTime? DateUpdate { get; set; }

        [Display(Name = "Ngày Tạo")]
        public DateTime? DateCreate { get; set; }
    }

}
