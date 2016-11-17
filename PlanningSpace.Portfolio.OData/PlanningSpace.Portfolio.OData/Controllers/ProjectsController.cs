using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using PlanningSpace.Portfolio.OData.Models;

namespace PlanningSpace.Portfolio.OData.Controllers
{
    [EnableQuery]
    public class ProjectsController : ODataController
    {
        public IQueryable<Project> GetProjects()
        {
            return DataRepository.DataRepository.Instance.GetProjects().AsQueryable();
        }

        public HttpResponseMessage Get([FromODataUri]int key)
        {
            Project project = DataRepository.DataRepository.Instance.GetProjects().Where(p => p.Id == key).SingleOrDefault();
            if (project == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, project);
        }

        public IQueryable<Property> GetProperties([FromODataUri]int key)
        {
            Project project = DataRepository.DataRepository.Instance.GetProjects().Where(p => p.Id == key).SingleOrDefault();
            return project.Properties.AsQueryable();
        }

    }
}