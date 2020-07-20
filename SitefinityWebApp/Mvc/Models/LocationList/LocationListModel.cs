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


namespace SitefinityWebApp.Mvc.Models.LocationList
{
   
    public class LocationListModel
    {
      
        public string Title { get; set; }

        public bool GreyBG { get; set; }
      
        public string SelectedUSP { get; set; }
        public string SelectedUSPId { get; set; }

        public LocationListViewModel GetViewModel()
        {
            return new LocationListViewModel()
            {
                Title = this.Title,  
                GreyBG = this.GreyBG,        
                LocationItems = this.GetUSPItems()
            };
        }



        protected virtual List<Location> GetUSPItems()
        {
            List<Location> items = new List<Location>();

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
                Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.OfficeLocations.Location");
                var item = dynamicModuleManager.GetDataItem(itemType, Guid.Parse(data["Id"].ToString()));


                if (item != null)
                {

                    var loc = new Location()
                    {
                        Title = item.GetValue("Title").ToString(),
                        Address = item.GetValue("Address").ToString(),
                        PhoneNumber = item.GetValue("PhoneNumber").ToString(),
                        EmailAddress = item.GetValue("EmailAddress").ToString()
                    };
                    items.Add(loc);
                }

            }
            return items;
        }
    }
}
