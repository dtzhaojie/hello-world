using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using PlanningSpace.Portfolio.OData.Models;

namespace PlanningSpace.Portfolio.OData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes();
            config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.EnsureInitialized();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new
            //    {
            //        id = RouteParameter.Optional
            //    }
            //);
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder
            {
                Namespace = "PlanningSpace.Portfolio.OData",
                ContainerName = "DefaultContainer"
            };
            builder.EntitySet<Project>("Projects");
            builder.EntitySet<Property>("Properties");

            return builder.GetEdmModel();
        }
    }
}
