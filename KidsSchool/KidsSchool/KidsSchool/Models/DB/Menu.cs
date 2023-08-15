namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            Menus1 = new HashSet<Menu>();
        }

        public int Id { get; set; }

        public int? LocationId { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        public int Target { get; set; }

        public int? OrderBy { get; set; }

        public int? ParentId { get; set; }

        public DateTime? DateUpdate { get; set; }

        public DateTime? DateCreate { get; set; }

        public virtual MenuLocation MenuLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menus1 { get; set; }

        public virtual Menu Menu1 { get; set; }
    }
}
