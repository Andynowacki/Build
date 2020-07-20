using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models.LogoList
{
    public class LogoListViewModel
    {
        public string Title { get; set; }

        public bool GreyBG { get; set; }

        public List<Logo> LogoItems { get; set; }
    }
}
