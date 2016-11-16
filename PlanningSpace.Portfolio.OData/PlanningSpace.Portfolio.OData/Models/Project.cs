using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlanningSpace.Portfolio.OData.Models
{
    public class Project
    {
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Name
        {
            get;
            set;
        }

        public int StartYear
        {
            get;
            set;
        }

        public int Duration
        {
            get;
            set;
        }
    }
}