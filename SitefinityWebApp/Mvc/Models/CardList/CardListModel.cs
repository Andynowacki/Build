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
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace SitefinityWebApp.Mvc.Models.CardList
{
   
    public class CardListModel
    {



        public string Title { get; set; }
        public string Content { get; set; }
        public bool CenterText { get; set; }

        public string SelectedUSP { get; set; }
        public string SelectedUSPId { get; set; }


        public CardListViewModel GetViewModel()
        {
            return new CardListViewModel()
            {
               
                Title  = this.Title,
                Content = this.Content,
                CenterText = this.CenterText,
                CardItems = this.GetCardItems()
            };
        }

   



        protected virtual List<Page> GetCardItems()
        {
            List<Page> items = new List<Page>();

            if (this.SelectedUSP == null)
            {
                return items;
            }

            var parsedItems = JArray.Parse(this.SelectedUSP);
            var contentLinksManager = ContentLinksManager.GetManager();
            var librariesManager = LibrariesManager.GetManager();
            foreach (var data in parsedItems)
            {
                PageManager pageManager = PageManager.GetManager();

                PageData page = pageManager.GetPageDataList()
                 .Where(pD => (pD.NavigationNode.Id == Guid.Parse(data["Id"].ToString()) && pD.Status == ContentLifecycleStatus.Live))
                 .FirstOrDefault();

                if (page != null)
                {



                    Image imageField = page.NavigationNode.GetRelatedItems<Image>("CardImageOrIcon").SingleOrDefault();
                    string ImageUrl = string.Empty;
                    string ImageAltText = string.Empty;

                    if (imageField != null)
                    {

                        // Work with image object
                        ImageUrl = imageField.ResolveMediaUrl();
                        ImageAltText = imageField.AlternativeText;

                    }





                    string cardtitle = page.NavigationNode.GetValue("CardTitle").ToString();
                    var card = new Page()
                    { 
                        CardDescription = page.NavigationNode.GetValue("CardDescription").ToString(),
                        CardButtonText = page.NavigationNode.GetValue("CardButtonText") == null || page.NavigationNode.GetValue("CardButtonText").ToString() == string.Empty ? "Read more" : page.NavigationNode.GetValue("CardButtonText").ToString(),                      
                        CardTitle = cardtitle ==  string.Empty ? page.NavigationNode.Title.ToString() : cardtitle,
                        ImageUrl = ImageUrl,
                        ImageAlt = ImageAltText,
                        LinkLabel = page.NavigationNode.Title,
                        LinkUrl = page.NavigationNode.GetDefaultUrl()

                     };
                items.Add(card);
            

            }
            }
            return items;
        }
    }
}
