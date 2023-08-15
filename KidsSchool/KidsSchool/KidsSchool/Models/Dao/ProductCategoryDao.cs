using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KidsSchool.Models.DB;
namespace KidsSchool.Models.Dao
{
    public static class ProductCategoryDao
    {
        public static Entities db = new Entities();
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var identifiedKeys = new HashSet<TKey>();
            return source.Where(element => identifiedKeys.Add(keySelector(element)));
        }

        public static bool CheckedCate(string ListCatId, int? catid)
        {
            var result = false;
            try
            {
                if (!string.IsNullOrEmpty(ListCatId) && catid!=null)
                {
                    string[] Listcat = ListCatId.Trim().Split(',');
                    foreach (var info in Listcat)
                    {
                        if (Convert.ToInt32(info) == catid)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch { }
            return result;
        }

        public static string ListCat(string CatId)
        {
            var str = "";
            try
            {
                if (CatId != null)
                {
                    string[] Listcat = CatId.Trim().Split(',');
                    foreach (var info in Listcat)
                    {
                        if (info != "")
                        {
                           str+= ListCatv2(CatId, Convert.ToInt32(info));
                        }
                    }
                }
            }
            catch { }
            return str;
        }
        public static string ListCatv2(string CatId,int? catid)
        {
            string str = "";
            var cate = db.ProductCategories.Find(catid);
            if (cate.ParentId != null)
            {
                if (!CatId.Contains("," + cate.ParentId + ","))
                {
                    str += CatId + cate.ParentId.ToString() + ",";
                    ListCatv2(str, cate.ParentId);

                }
            }else
            {
                return CatId;
            }
            return str;
        }
        public static string NameTagCate(string catId)
        {
            var str = "";
            var listCate = db.ProductCategories.OrderBy(x=>x.DisplayOrder);
            string[] Listcat = catId.Trim().Split(',');
            foreach (var cateId in Listcat)
            {
                if (cateId != "")
                {
                    try
                    {
                        if(Int32.TryParse(cateId, out Int32 Id))
                         {
                            str += listCate.FirstOrDefault(x => x.CatId == Id).Name + ", ";
                        }
                    }
                    catch { str += "Not cate"; }
                }
            }
            return str;
        }
        public static SelectList listCategory(int? selected = null, int exclude_id = -1)
        {
            //return new SelectList(new[] { new SelectListItem() { Text = "No Category", Value = "0" } });

            return new SelectList(db.ProductCategories.Where(p => p.CatId != exclude_id), "CatId", "Name", selected);
        }

        public static List<int> ChildCategoryIds(int _parentId)
        {
            List<int> ListCats = new List<int>();
            ListCats.Add(_parentId);

            foreach (var cat in db.ProductCategories.Where(p => p.ParentId == _parentId &&  !p.isDelete).ToList())
            {
                int _catid = Convert.ToInt32(cat.CatId);
                ListCats.Add(_catid);
            }

            return ListCats;
        }
        public static List<ProductCategory> ChildCategories(int CatId)
        {
            try
            {
                return db.ProductCategories.Where(c => c.ParentId == CatId && !c.isDelete).OrderBy(c => c.Name).ToList();
            }
            catch
            {
                return null;
            }
        }
        public static List<ProductCategory> ChildCategories(this ProductCategory parentCategory, int Limit = 20)
        {
            try {
                return db.ProductCategories.Where(c => c.ParentId == parentCategory.CatId&& !c.isDelete).OrderBy(c => c.Name).Take(Limit).ToList();
            }
            catch { return null; }
            }


        public static List<Product> SearchJoin(this List<Product> listProduct, string[] keyword)
        {
            foreach (var key in keyword)
            {
                if (key != "9991" && key != "9992" && key != "9993")
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        listProduct = ProductSearch(listProduct, key);
                    } 
                }
            }
            return listProduct;
        }

        public static List<Product> ProductSearch(this List<Product> listProduct, string key)
        {
            //var listcatidslipts = key.ToString().Split(',').ToList();
            //listProduct = listProduct.Where(x => x.CatId.Split(',').OrderBy(k => k).ToList().Any(l => listcatidslipts.Any(id => l == id))).ToList();
            listProduct = listProduct.Where(x => x.CatId.Split(',').OrderBy(k => k).ToList().Any(l => l == key)).ToList();
            return listProduct;
        }



        public static List<Product> AllProducts(this ProductCategory category, int Limit = 200)
        {
            var productsAll = DataPuplic.product.Where(x => x.CatId.Split(',').Any(c => c == category.CatId.ToString() && (x.AutoPostDate == null || x.AutoPostDate < DateTime.Now)) && x.StatusId == 1 && !x.IsDelete).ToList();
            return productsAll.OrderBy(X => X.Views).Take(Limit).ToList();
        }

        //public static int CountProducts(this ProductCategory category)
        //{
        //    var productsAll = db.Products.Where(x => x.CatId.Contains(category.CatId.ToString()) && x.StatusId == 1 && !x.IsDelete).ToList();
        //    var productsmid = productsAll.Where(x => x.CatId.Contains("," + category.CatId + ",")).ToList().Count;
        //    var productsFirst = productsAll.Where(p => p.CatId.Substring(0, p.CatId.IndexOf(",")).Contains(category.CatId.ToString()) && p.StatusId == 1).ToList().Count;
        //    var productsLast = productsAll.Where(p => p.CatId.Substring(p.CatId.LastIndexOf(",")).Contains(category.CatId.ToString()) && p.StatusId == 1).ToList().Count;

        //    return productsmid+ productsFirst+ productsLast;
        //}
    }
}