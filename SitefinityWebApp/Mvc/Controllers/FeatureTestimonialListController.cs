using SitefinityWebApp.Mvc.Models.FeatureTestimonialList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Feature Testimonial", Title = "Feature Testimonial", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class FeatureTestimonialListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public FeatureTestimonialListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new FeatureTestimonialListModel();
                }

                return this.model;
            }
        }

        public string Template
        {
            get { return this.template; }
            set { this.template = value; }
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        // GET: TextImageSplit

        public ActionResult Index()
        {
            return View("FeatureTestimonialList." + this.template, this.Model.GetViewModel());
        }

        private FeatureTestimonialListModel model;
        private string template = "Default";
    }
}
