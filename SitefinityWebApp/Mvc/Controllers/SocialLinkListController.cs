using SitefinityWebApp.Mvc.Models.SocialLinkList;
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
    [ControllerToolboxItem(Name = "SocialLink", Title = "Social Links", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class SocialLinkListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SocialLinkListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new SocialLinkListModel();
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
            return View("SocialLinkList." + this.template, this.Model.GetViewModel());
        }

        private SocialLinkListModel model;
        private string template = "Default";
    }
}
