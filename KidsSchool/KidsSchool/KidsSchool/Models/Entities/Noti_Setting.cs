namespace KidsSchool.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Noti_Setting
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string key_server { get; set; }

        [Column("Sender ID SenderId")]
        [StringLength(50)]
        public string Sender_ID__SenderId { get; set; }

        [StringLength(500)]
        public string ApiKey { get; set; }

        [StringLength(500)]
        public string AppId { get; set; }
    }
}
