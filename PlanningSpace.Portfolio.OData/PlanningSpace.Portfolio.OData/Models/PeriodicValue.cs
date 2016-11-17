using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanningSpace.Portfolio.OData.Models
{
    public class PeriodicValue
    {
        public string Name
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}