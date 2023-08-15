namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Testimonial
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Message { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAvatar { get; set; }
    }
}
