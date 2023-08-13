namespace KidsSchool.Models.Entities
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

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string CusName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(350)]
        public string Content { get; set; }

        public int? GroupId { get; set; }

        public DateTime? Upd_Date { get; set; }

        public bool IsDelete { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateCreate { get; set; }

        public virtual ContactGHelpGroup ContactGHelpGroup { get; set; }
    }
}
