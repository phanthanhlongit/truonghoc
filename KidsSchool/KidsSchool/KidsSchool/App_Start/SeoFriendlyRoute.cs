using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace KidsSchool
{
    public class SeoFriendlyRoute : Route
    {
        public SeoFriendlyRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
        {
        }
        public SeoFriendlyRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler, string[] namespaces) : base(url, defaults, routeHandler)
        {
            DataTokens = defaults;
            defaults["Namespaces"] = namespaces;
        }
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);

            if (routeData != null)
            {
                if (routeData.Values.ContainsKey("Slug"))
                    routeData.Values["Slug"] = GetIdValue(routeData.Values["Slug"]);
            }

            return routeData;
        }

        private object GetIdValue(object Slug)
        {
            if (Slug != null)
            {
                string idValue = Slug.ToString();
                return idValue;
                //string[] tokens = idValue.Split('-');
                //return tokens.Last();
            }
            return Slug;
        }
    }
}