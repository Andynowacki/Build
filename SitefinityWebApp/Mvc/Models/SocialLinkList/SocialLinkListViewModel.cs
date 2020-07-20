using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models.SocialLinkList
{
    public class SocialLinkListViewModel
    {
        public string Title { get; set; }



        public List<SocialLink> SocialLinkItems { get; set; }
    }
}
