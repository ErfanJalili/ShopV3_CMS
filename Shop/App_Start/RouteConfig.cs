using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             "SortBy",
             "pages/SortBy/{id}/{categoryId}/{brandId}",
             new { controller = "Pages", action = "SortBy" });

            routes.MapRoute(
              "Brand",
              "pages/brand/{id}",
              new { controller = "Pages", action = "ByBrand" });

            routes.MapRoute(
                "Category",
                "pages/category/{id}",
                new { controller="Pages",action="ByCategory"});

            routes.MapRoute(
                "Product",
                "pages/SingleProduct/{id}",
                new { controller = "Pages", action = "SingleProduct" });

            routes.MapRoute(
          "AddToCard",
          "orders/AddToCard/SelectedProduct={productId}/YourOrderId={orderId}",
          new { controller = "orders", action = "AddToCard", orderId = UrlParameter.Optional });

            routes.MapRoute(
            "filter",
            "pages/filter/{query}/{categoryId}/{brandId}",
            new { controller = "Pages", action = "SortBy" });
        }
    }
}
