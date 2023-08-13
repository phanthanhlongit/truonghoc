namespace KidsSchool.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Banner
    {
        public int BannerId { get; set; }

        public string Name { get; set; }

        public int BannerTypeId { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Active { get; set; }

        public int BannerPositionId { get; set; }

        public int ItemFor { get; set; }

        public int ItemForId { get; set; }

        public DateTime? DateUpdate { get; set; }

        public DateTime? DateCreate { get; set; }

        public virtual BannerPosition BannerPosition { get; set; }

        public virtual BannerType BannerType { get; set; }
    }
}
