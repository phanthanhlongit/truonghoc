using KidsSchool.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KidsSchool.Models.Dao
{
    public class ProductDao
    {
        public static Entities db = new Entities();
        public static string catchu(string s, int kytu)
        {
            if (s.Length > kytu)
            {
                return s.Substring(0, kytu) + "...";
            }
            if (s.Length < 20)
            {
                return s + "<br/>";
            }
            return s;
        }

        public static string barecumCate(string catid)
        {
            var str = "";
            try
            {
                var iscateParent = false;
                string[] Listcat = catid.Trim().Split(',');
                var cate = db.ProductCategories;
                var catedanhmuc = cate.Where(x => x.Name.ToLower().Contains("danh mục")).ToList();
                foreach (var info in Listcat)
                {
                    if (info != ""&& Int32.TryParse(info, out int idCate))
                    {
                        if (catedanhmuc.Count > 0)
                        {
                                var Parentid = catedanhmuc[0].CatId;
                                var cateparent = cate.Where(x => x.ParentId == Parentid && x.CatId == idCate).ToList();
                                if (cateparent.Count > 0)
                                {
                                    str += "<li><a href=\"/danh-muc-san-pham-" + cateparent[0].Slug + "\" title=\"" + cateparent[0].Name + "\">" + cateparent[0].Name + "</a></li>";
                                    iscateParent = true;
                                    break;
                                }
                                else
                                {
                                    if (iscateParent == false)
                                    {
                                        var cateparent2 = cate.Where(x => x.ParentId == idCate).ToList();
                                        if (cateparent2.Count > 0)
                                        {
                                            str += "<li><a href=\"/danh-muc-san-pham-" + cateparent2[0].Slug + "\" title=\"" + cateparent[0].Name + "\">" + cateparent[0].Name + "</a></li>";
                                            iscateParent = true;
                                            break;
                                        }
                                    }
                                }
                        }
                    }
                }
                return str;
            }
             catch (Exception ex) 
            {
                return str;
            }
        }
        public static string TagLink(string Tag)
        {
          
            var str = "";
            try
            {
                if (Tag != null)
                {
                    string[] Listcat = Tag.Trim().Split(',');
                    var i = 1;
                    foreach (var info in Listcat)
                    {
                        if (info != "")
                        {
                            try
                            {
                                str += "<a href=\"/tim-kiem-san-pham?Keyword=" + info.ToAscii() + "\" title=\"" + info + "\">" + info + "</a>";

                            }
                            catch { str += "Not Tag"; }
                        }

                        if(i< Listcat.Count())
                        {
                            str += ", ";
                        } 
                        
                        i++;
                    }
                }
            }
            catch { }
            return str;
        }
        public static IQueryable<Product> SearchByName(string term)
        {
            IQueryable<Product> lst;
            lst = db.Products.Where(u => u.Name.Contains(term)&&u.StatusId==1);
            return lst;
        }


        public static IQueryable<Product> AdvancedSearch(string term, string loai, string hangsx, int? minprice, int? maxprice)
        {
            IQueryable<Product> lst = db.Products;
            if (!string.IsNullOrEmpty(term))
                lst = SearchByName(term);
            //if (!string.IsNullOrEmpty(loai))
            //    lst = from p in lst where p.LoaiSP.Equals(loai) select p;
            //if (!string.IsNullOrEmpty(hangsx))
            //    lst = from p in lst where p.HangSX.Equals(hangsx) select p;
            //if (minprice != null)
            //    lst = from p in lst where p.GiaTien >= minprice select p;
            //if (maxprice != null)
            //    lst = from p in lst where p.GiaTien <= maxprice select p;
            return lst;
        }
        public static IQueryable<Product> SearchByType(string term)
        {
            var splist = (from p in db.Products where p.ProductType.Equals(term) && p.StatusId==1 select p);
            return splist;
        }
        private static string TaoMa()
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "";
                for (int i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }

        private static bool KiemtraID(string maID)
        {
                var temp = db.Products.Find(maID);
                if (temp == null)
                    return true;
                return false;
        }

        public static string AttributeByName(int id)
        {
            var str = "";
            var obj = db.Attributes.FirstOrDefault(x => x.Id == id);
            if(obj!=null)
            {
                str = obj.Name;
            }    
            return str;
        }
    }
}