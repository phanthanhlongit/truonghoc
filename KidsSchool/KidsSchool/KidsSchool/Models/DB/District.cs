namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class District
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public District()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        [StringLength(255)]
        public string districtid { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        [StringLength(255)]
        public string location { get; set; }

        [StringLength(255)]
        public string provinceid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }

        public virtual Province Province { get; set; }
    }
}
