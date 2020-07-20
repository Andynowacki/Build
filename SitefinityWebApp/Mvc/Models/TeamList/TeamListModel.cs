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


namespace SitefinityWebApp.Mvc.Models.TeamList
{
   
    public class TeamListModel
    {
      
            
        public string SelectedItem { get; set; }
        public string SelectedItemId { get; set; }

        public TeamListViewModel GetViewModel()
        {
            return new TeamListViewModel()
            {
                     
                TeamItems = this.GetTeamItems()
            };
        }



        protected virtual List<Team> GetTeamItems()
        {
            List<Team> items = new List<Team>();

            if (this.SelectedItem == null)
            {
                return items;
            }

            var parsedItems = JArray.Parse(this.SelectedItem);
            var contentLinksManager = ContentLinksManager.GetManager();
            var librariesManager = LibrariesManager.GetManager();
            foreach (var data in parsedItems)
            {
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
                Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.ExecutiveMembers.ExecutiveMember");
                var item = dynamicModuleManager.GetDataItem(itemType, Guid.Parse(data["Id"].ToString()));


                if (item != null)
                {


                Image imageField = item.GetRelatedItems<Image>("Image").SingleOrDefault();
                string ImageUrl = string.Empty;
                string ImageAltText = string.Empty;

                if (imageField != null)
                {

                    // Work with image object
                    ImageUrl = imageField.ResolveMediaUrl();
                    ImageAltText = imageField.AlternativeText;




                }


               var Team = new Team()
                    {
                        Title = item.GetValue("Title").ToString(),
                        Description = item.GetValue("Description").ToString(),
                        JobTitle = item.GetValue("JobTitle").ToString(),
                        ImageAlt = ImageAltText,
                        ImageUrl = ImageUrl,
                        LinkedinUrl = item.GetValue("LinkedinUrl").ToString(),
                        

                    };
                    items.Add(Team);
                }

            }
            return items;
        }
    }
}
