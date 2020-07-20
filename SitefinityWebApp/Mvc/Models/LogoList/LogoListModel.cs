using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.DynamicModules;

using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Utilities.TypeConverters;

using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Libraries.Model;


namespace SitefinityWebApp.Mvc.Models.LogoList
{ 
    public class LogoListModel
    {
      
        public string Title { get; set; }

        public bool GreyBG { get; set; }
      
        public string SelectedUSP { get; set; }
        public string SelectedUSPId { get; set; }

        public LogoListViewModel GetViewModel()
        {
            return new LogoListViewModel()
            {
                Title = this.Title,  
                LogoItems = this.GetLogoItems()
            };
        }



        protected virtual List<Logo> GetLogoItems()
        {
            List<Logo> items = new List<Logo>();

            if (this.SelectedUSP == null)
            {
                return items;
            }

            var parsedItems = JArray.Parse(this.SelectedUSP);
            var contentLinksManager = ContentLinksManager.GetManager();
            var librariesManager = LibrariesManager.GetManager();
            foreach (var data in parsedItems)
            {
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
                Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.LogoGallery.Logo");
                var item = dynamicModuleManager.GetDataItem(itemType, Guid.Parse(data["Id"].ToString()));


                if (item != null)
                {


                Image imageField = item.GetRelatedItems<Image>("Logo").SingleOrDefault();
                string ImageUrl = string.Empty;
                string ImageAltText = string.Empty;

                if (imageField != null)
                {

                    // Work with image object
                    ImageUrl = imageField.ResolveMediaUrl();
                    ImageAltText = imageField.AlternativeText;




                }


               var logo = new Logo()
                    {
                        
                        ImageAlt = ImageAltText,
                        ImageUrl = ImageUrl,
                        LinkLabel = item.GetValue("LinkLabel").ToString(),
                        LinkUrl = item.GetValue("LinkUrl").ToString()

                    };
                    items.Add(logo);
                }

            }
            return items;
        }
    }
}
