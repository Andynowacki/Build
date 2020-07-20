using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models.USPList
{
    public class ContactListViewModel
    {
        public string Title { get; set; }

        public bool GreyBG { get; set; }

        public List<USP> USPItems { get; set; }
    }
}
