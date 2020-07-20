using SitefinityWebApp.Mvc.Models.USPList;
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
    [ControllerToolboxItem(Name = "USPList", Title = "USP List", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class USPListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public USPListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new USPListModel();
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
            return View("USPList." + this.template, this.Model.GetViewModel());
        }

        private USPListModel model;
        private string template = "Default";
    }
}
