using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicTypes.Model;

namespace SitefinityWebApp.Mvc.Models
{
    public class BannerViewModel
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public CustomImageModel Image { get; set; }
   
        public string Link { get; set; }

        public string YouTubeId { get; set; }

    }
}