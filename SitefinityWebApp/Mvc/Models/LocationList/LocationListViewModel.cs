using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models.LocationList
{
    public class LocationListViewModel
    {
        public string Title { get; set; }

        public bool GreyBG { get; set; }

        public List<Location> LocationItems { get; set; }
    }
}
