using SitefinityWebApp.Mvc.Models.TestimonialList;
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
    [ControllerToolboxItem(Name = "Testimonial", Title = "Testimonial", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class TestimonialListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TestimonialListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new TestimonialListModel();
                }

                return this.model;
            }
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }
        public string Template
        {
            get { return this.template; }
            set { this.template = value; }
        }

        // GET: TextImageSplit
      
        public ActionResult Index()
        {
            return View("TestimonialList." + this.template, this.Model.GetViewModel());
        }

        private TestimonialListModel model;
        private string template = "Default";
    }
}
