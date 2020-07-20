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


namespace SitefinityWebApp.Mvc.Models.USPList
{
   
    public class ContactListModel
    {
      
        public string Title { get; set; }

        public bool GreyBG { get; set; }
      
        public string SelectedUSP { get; set; }
        public string SelectedUSPId { get; set; }

        public ContactListViewModel GetViewModel()
        {
            return new ContactListViewModel()
            {
                Title = this.Title,  
                GreyBG = this.GreyBG,        
                USPItems = this.GetUSPItems()
            };
        }



        protected virtual List<USP> GetUSPItems()
        {
            List<USP> items = new List<USP>();

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
                Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.USP.Usp");
                var item = dynamicModuleManager.GetDataItem(itemType, Guid.Parse(data["Id"].ToString()));


                if (item != null)
                {


                Image imageField = item.GetRelatedItems<Image>("Icon").SingleOrDefault();
                string ImageUrl = string.Empty;
                string ImageAltText = string.Empty;

                if (imageField != null)
                {

                    // Work with image object
                    ImageUrl = imageField.ResolveMediaUrl();
                    ImageAltText = imageField.AlternativeText;




                }


               var usp = new USP()
                    {
                        Title = item.GetValue("Title").ToString(),
                        Description = item.GetValue("Description").ToString(),
                        ImageAlt = ImageAltText,
                        ImageUrl = ImageUrl,
                        LinkLabel = item.GetValue("LinkLabel").ToString(),
                        LinkUrl = item.GetValue("LinkUrl").ToString()

                    };
                    items.Add(usp);
                }

            }
            return items;
        }
    }
}
