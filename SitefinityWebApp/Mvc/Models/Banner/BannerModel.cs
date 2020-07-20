using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Mvc.Models
{
    public class BannerModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ImageId { get; set; }
        public string ImageProviderName { get; set; }
        public string Link { get; set; }

        public string YouTubeId { get; set; }

        public BannerModel()
        {
            if (string.IsNullOrEmpty(this.Title))
            {
                var SiteMapProvider = SiteMapBase.GetCurrentProvider();

                if (SiteMapProvider.CurrentNode != null)
                {
                    this.Title = SiteMapProvider.CurrentNode.Title;
                }
            }
        }
   
        public BannerViewModel GetViewModel()
        {
            var ImageModel = new CustomImageModel();
            SfImage image;

            if (this.ImageId != Guid.Empty)
            {
                image = this.GetImage();
                if (image != null)
                {
                    ImageModel.SelectedSizeUrl = this.GetSelectedSizeUrl(image);
                    ImageModel.ImageAlternativeText = image.AlternativeText;
                    ImageModel.ImageTitle = image.Title;
                }
            }

            string Content = null;

            if (this.Description != null)
            {
                Content = Regex.Replace(this.Description, @"\r\n?|\n", "<br>");
            }

            return new BannerViewModel()
            {
                Title = this.Title,
             
                Description = Content,
                Image = ImageModel,                
                Link = this.Link,
                YouTubeId = this.YouTubeId
            };
        }

        protected virtual SfImage GetImage()
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager(this.ImageProviderName);

            return librariesManager.GetImage(this.ImageId);

        }

        protected virtual string GetSelectedSizeUrl(SfImage image)
        {
            if (image.Id == Guid.Empty)
            {
                return string.Empty;
            }

            string imageUrl;

            var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
            var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
            imageUrl = originalImageUrl;

            return imageUrl;
        }

       
    }
}
