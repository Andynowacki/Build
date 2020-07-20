using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
using System.Text.RegularExpressions;


namespace SitefinityWebApp.Mvc.Models
{
    public class ButtonModel
    {

    
        public string Link { get; set; }
   
        public ButtonViewModel GetViewModel()
        {
            

            return new ButtonViewModel()
            {
                               
                Link = this.Link
                
            };
        }

      

       
    }
}