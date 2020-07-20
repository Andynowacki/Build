using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SitefinityWebApp.Mvc.Models
{
    public class TextImageSplitModel
    {
        public string Subtitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ImageId { get; set; }
        public string ImageProviderName { get; set; }
        public bool IsImageFirst { get; set; }
        public string Link { get; set; }
   
        public TextImageSplitViewModel GetViewModel()
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

            if (this.Content != null)
            {
                Content = Regex.Replace(this.Content, @"\r\n?|\n", "<br>");
            }

            return new TextImageSplitViewModel()
            {
                Title = this.Title,
                Subtitle = this.Subtitle,
                Content = Content,
                Image = ImageModel,
                IsImageFirst = this.IsImageFirst,
                Link = this.Link
                
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