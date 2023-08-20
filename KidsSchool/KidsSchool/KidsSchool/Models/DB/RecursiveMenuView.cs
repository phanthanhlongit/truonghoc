namespace KidsSchool.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class RecursiveMenuView
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int? Target { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }
        public string Path { get; set; }
    }

}