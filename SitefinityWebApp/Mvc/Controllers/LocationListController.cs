using SitefinityWebApp.Mvc.Models.LocationList;
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
    [ControllerToolboxItem(Name = "LocationList", Title = "List of Offices Locations", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class LocationListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LocationListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new LocationListModel();
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
            return View("LocationList." + this.template, this.Model.GetViewModel());
        }

        private LocationListModel model;
        private string template = "Default";
    }
}
