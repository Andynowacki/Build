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


namespace SitefinityWebApp.Mvc.Models.FeatureTestimonialList
{
   
    public class FeatureTestimonialListModel
    {
        public bool ContentLeft { get; set; }
      
        public string SelectedTestimonial { get; set; }
        public string SelectedTestimonialId { get; set; }

        public FeatureTestimonialListViewModel GetViewModel()
        {
            return new FeatureTestimonialListViewModel()
            {
                ContentLeft = this.ContentLeft,
                TestimonialList = this.GetTestimonialItems()
            };
        }

        //protected virtual List<Testimonial> GetTestimonials()
        //{
        //    if (this.SelectedTestimonial == null)
        //    {
        //        return null;
        //    }

        //    var data = JObject.Parse(this.SelectedTestimonial);

        //    if (data == null)
        //    {
        //        return null;
        //    }

        //    var contentLinksManager = ContentLinksManager.GetManager();
        //    var librariesManager = LibrariesManager.GetManager();
        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
        //    Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial");
        //    var item = dynamicModuleManager.GetDataItem(itemType, Guid.Parse(data["Id"].ToString()));

        //    if (item == null)
        //    {
        //        return null;
        //    }

        //    Image bgimageField = item.GetRelatedItems<Image>("BackgroundImage").SingleOrDefault();
        //    string bgImageUrl = string.Empty;
        //    string bgImageAltText = string.Empty;

        //    Image imageField = item.GetRelatedItems<Image>("Logo").SingleOrDefault();
        //    string ImageUrl = string.Empty;
        //    string ImageAltText = string.Empty;

        //    if (imageField != null)
        //    {
        //        // Work with image object
        //        ImageUrl = imageField.ResolveMediaUrl();
        //        ImageAltText = imageField.AlternativeText;
        //    }


        //    if (bgimageField != null)
        //    {
        //        // Work with image object
        //        bgImageUrl = bgimageField.ResolveMediaUrl();
        //        bgImageAltText = bgimageField.AlternativeText;
        //    }



        //    var testimonial = new Testimonial()
        //    {
        //        BGImageAlt = bgImageAltText,
        //        BGImageUrl = bgImageUrl,
        //        ImageAlt = ImageAltText,
        //        ImageUrl = ImageUrl,
        //        JobTitle = item.GetValue("JobTitle").ToString(),
        //        Quote = item.GetValue("Quote").ToString(),
        //        Name = item.GetValue("Name").ToString(),
        //        YouTubeLink =item.GetValue("YouTubeLink").ToString()
        //    };

        //    return testimonial;
        //}





        protected virtual List<Testimonial> GetTestimonialItems()
        {
            List<Testimonial> items = new List<Testimonial>();

            if (this.SelectedTestimonial == null)
            {
                return items;
            }

            var parsedItems = JArray.Parse(this.SelectedTestimonial);
            var contentLinksManager = ContentLinksManager.GetManager();
            var librariesManager = LibrariesManager.GetManager();
            foreach (var data in parsedItems)
            {
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
                Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial");
                var item = dynamicModuleManager.GetDataItem(itemType, Guid.Parse(data["Id"].ToString()));


                if (item != null)
                {

                    Image bgimageField = item.GetRelatedItems<Image>("BackgroundImage").SingleOrDefault();
                    string bgImageUrl = string.Empty;
                    string bgImageAltText = string.Empty;

                    Image imageField = item.GetRelatedItems<Image>("Logo").SingleOrDefault();
                    string ImageUrl = string.Empty;
                    string ImageAltText = string.Empty;

                    if (imageField != null)
                    {

                        // Work with image object
                        ImageUrl = imageField.ResolveMediaUrl();
                        ImageAltText = imageField.AlternativeText;

                    }


                    if (bgimageField != null)
                    {

                        // Work with image object
                        bgImageUrl = bgimageField.ResolveMediaUrl();
                        bgImageAltText = bgimageField.AlternativeText;

                    }



                    var testimonial = new Testimonial()
                    {
                        BGImageAlt = bgImageAltText,
                        BGImageUrl = bgImageUrl,
                        ImageAlt = ImageAltText,
                        ImageUrl = ImageUrl,
                        JobTitle = item.GetValue("JobTitle").ToString(),
                        Quote = item.GetValue("Quote").ToString(),
                        Name = item.GetValue("Title").ToString(),
                        YouTubeLink = item.GetValue("YouTubeLink").ToString()

                    };
                    items.Add(testimonial);
                }

            }
            return items;
        }
    }
}
