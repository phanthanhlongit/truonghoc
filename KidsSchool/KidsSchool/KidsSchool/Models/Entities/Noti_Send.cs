namespace KidsSchool.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Noti_Send
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Icon { get; set; }

        [StringLength(500)]
        public string Sound { get; set; }

        public DateTime? Date { get; set; }

        public int? Status { get; set; }

        [Column(TypeName = "text")]
        public string ToListId { get; set; }
    }
}
