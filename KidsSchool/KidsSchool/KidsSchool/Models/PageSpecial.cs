using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KidsSchool.Models
{
    public partial class WebsiteData
    {
        public int Stt { get; set; }
        public string Images { get; set; }
        public List<string> H1 { get; set; }
        public List<string> H2 { get; set; }
        public List<string> H3 { get; set; }
        public List<string> H4 { get; set; }
        public List<string> H5 { get; set; }
        public List<string> OtherTags { get; set; }
        public WebsiteData()
        {
            H1 = new List<string>();
            H2 = new List<string>();
            H3 = new List<string>();
            H4 = new List<string>();
            H5 = new List<string>();
            OtherTags = new List<string>();
        }
    }
}