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


namespace SitefinityWebApp.Mvc.Models.SocialLinkList
{
   
    public class SocialLinkListModel
    {
      
        public string Title { get; set; }


      
        public string SelectedUSP { get; set; }
        public string SelectedUSPId { get; set; }

        public SocialLinkListViewModel GetViewModel()
        {
            return new SocialLinkListViewModel()
            {
                Title = this.Title,  
                  
                SocialLinkItems = this.GetUSPItems()
            };
        }



        protected virtual List<SocialLink> GetUSPItems()
        {
            List<SocialLink> items = new List<SocialLink>();

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
                Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Socialmediafooter.Sociallink");
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


               var slink = new SocialLink()
                    {
                       
                       
                        ImageAlt = ImageAltText,
                        ImageUrl = ImageUrl,
                        LinkLabel = item.GetValue("LinkLabel").ToString(),
                        LinkUrl = item.GetValue("LinkUrl").ToString()

                    };
                    items.Add(slink);
                }

            }
            return items;
        }
    }
}
