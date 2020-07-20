using SitefinityWebApp.Mvc.Models.LogoList;
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
    [ControllerToolboxItem(Name = "LogoList", Title = "Logo Gallery", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class LogoListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LogoListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new LogoListModel();
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
            return View("LogoList." + this.template, this.Model.GetViewModel());
        }

        private LogoListModel model;
        private string template = "Default";
    }
}
