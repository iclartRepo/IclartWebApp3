﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IclartWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 "Home",
                 "home",
                 new {controller = "Home", action = "Index"}
           );

            /* Account Management */
            routes.MapRoute(
                "ChangePassword",
                "changepassword",
                new { controller = "Manage", action = "ChangePassword" }
            );

            routes.MapRoute(
              "ForgotPassword",
              "forgotpassword",
              new { controller = "Account", action = "ForgotPassword" }
              );

            routes.MapRoute(
            "ManageAccount",
            "accounts",
            new { controller = "Account", action = "ManageAccount" }
             );

            /* Client Management */

            routes.MapRoute(
           "ManageClient",
           "clients",
           new { controller = "Client", action = "Index" }
            );

            routes.MapRoute(
         "AddClient",
         "addclient",
         new { controller = "Client", action = "AddClient" }
          );

            routes.MapRoute(
    "UpdateClient",
    "updateclient/{id}",
    new { controller = "Client", action = "UpdateClient", id = UrlParameter.Optional }
     );

            routes.MapRoute(
     "ViewClient",
     "viewclient/{id}",
     new { controller = "Client", action = "ViewClient", id = UrlParameter.Optional }
      );

            /* Competitor Management */

            routes.MapRoute(
                 "ManageCompetitors",
                 "competitors",
                 new { controller = "Competitor", action = "Index" }
                  );

            /* Product Management */

            routes.MapRoute(
              "ManageProducts",
              "productadmin/{id}",
              new { controller = "Product", action = "Index", id = 1 }
               );

            routes.MapRoute(
           "ViewProduct",
           "productdetails/{id}",
           new { controller = "Product", action = "ViewProduct", id = UrlParameter.Optional  }
            );

            routes.MapRoute(
         "AddProduct",
         "addproduct",
         new { controller = "Product", action = "AddProduct" }
          );

            routes.MapRoute(
       "UpdateProduct",
       "updateProduct/{id}",
       new { controller = "Product", action = "UpdateProduct", id = UrlParameter.Optional }
        );

            /*SOS Management */
            routes.MapRoute(
             "ManageSOS",
             "sos",
             new { controller = "SOS", action = "Index" }
              );

            routes.MapRoute(
           "AddSOS",
           "addsos",
           new { controller = "SOS", action = "AddSOS" }
            );

            routes.MapRoute(
             "ViewSOS",
             "sosdetails/{id}",
             new { controller = "SOS", action = "ViewSOS", id = UrlParameter.Optional }
              );

            routes.MapRoute(
           "UpdateSOS",
           "updatesos/{id}",
           new { controller = "SOS", action = "UpdateSOS", id = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        } 
    }
}
