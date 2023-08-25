namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetRoles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; }

        [Display(Name = "Email")]
        [StringLength(256)]
        public string Email { get; set; }

        [Display(Name = "Xác nhận Email")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Mật khẩu")]
        public string PasswordHash { get; set; }

        [Display(Name = "Mã bảo mật")]
        public string SecurityStamp { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Xác nhận số điện thoại")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "Kích hoạt xác thực hai yếu tố")]
        public bool TwoFactorEnabled { get; set; }

        [Display(Name = "Ngày kết thúc khóa")]
        public DateTime? LockoutEndDateUtc { get; set; }

        [Display(Name = "Cho phép khóa")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "Số lần truy cập không thành công")]
        public int AccessFailedCount { get; set; }

        [Required]
        [Display(Name = "Tên đăng nhập")]
        [StringLength(256)]
        public string UserName { get; set; }

        [Display(Name = "Họ và tên")]
        [StringLength(256)]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(500)]
        public string Address { get; set; }

        [Display(Name = "Mã quận/huyện")]
        [StringLength(255)]
        public string DistrictId { get; set; }

        [Display(Name = "Phân loại")]
        [StringLength(128)]
        public string Discriminator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
