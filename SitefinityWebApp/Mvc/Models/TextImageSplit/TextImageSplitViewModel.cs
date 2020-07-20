using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicTypes.Model;

namespace SitefinityWebApp.Mvc.Models
{
    public class TextImageSplitViewModel
    {
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public CustomImageModel Image { get; set; }
        public bool IsImageFirst { get; internal set; }
        public string Link { get; set; }
        
    }
}