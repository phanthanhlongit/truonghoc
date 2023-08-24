namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactGHelp")]
    public partial class ContactGHelp
    {
        public int Id { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Display(Name = "Tên khách hàng")]
        [StringLength(250)]
        public string CusName { get; set; }

        [Display(Name = "Tên trẻ")]
        [StringLength(250)]
        public string ChilName { get; set; }

        [Display(Name = "Tuổi trẻ")]
        [StringLength(50)]
        public string ChilAge { get; set; }

        [Display(Name = "Email")]
        [StringLength(250)]
        public string Mail { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250)]
        public string Address { get; set; }

        [Display(Name = "Nội dung")]
        [StringLength(350)]
        public string Content { get; set; }

        [Display(Name = "Nhóm hỗ trợ")]
        public int? GroupId { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? Upd_Date { get; set; }

        [Display(Name = "Đã xóa")]
        public bool IsDelete { get; set; }

        [Display(Name = "Đã đọc")]
        public bool IsRead { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? DateCreate { get; set; }

        public virtual ContactGHelpGroup ContactGHelpGroup { get; set; }
    }
}
