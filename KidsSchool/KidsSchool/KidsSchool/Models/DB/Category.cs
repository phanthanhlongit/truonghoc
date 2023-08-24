namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Categories1 = new HashSet<Category>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        [Display(Name = "Tên")]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Đường dẫn thân thiện")]
        [StringLength(250)]
        public string Slug { get; set; }

        [Display(Name = "Mô tả")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "Tiêu đề Meta")]
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả Meta")]
        public string MetaDescription { get; set; }

        [Display(Name = "Từ khóa Meta")]
        public string MetaKeyword { get; set; }

        [Display(Name = "Script")]
        [AllowHtml]
        public string Script { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Display(Name = "Biểu tượng")]
        [StringLength(250)]
        public string Icon { get; set; }

        [Display(Name = "Đã xóa")]
        public bool IsDelete { get; set; }

        [Display(Name = "Hiển thị trang chủ")]
        public bool ShowIndex { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? DateUpdate { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? DateCreate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories1 { get; set; }

        public virtual Category Category1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }
    }

}
