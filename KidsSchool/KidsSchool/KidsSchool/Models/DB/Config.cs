namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Config")]
    public partial class Config
    {
        public string Icon { get; set; }
        public int id { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string MetaTitle { get; set; }

        public string Copyright { get; set; }
        public string UrlMap { get; set; }

        public int? CatId { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public string ScriptAllPage { get; set; }

        public string IndexScriptFooter { get; set; }

        public string IndexScriptHeader { get; set; }

        public DateTime? DateUpdate { get; set; }

        public DateTime? DateCreate { get; set; }
    }
}
