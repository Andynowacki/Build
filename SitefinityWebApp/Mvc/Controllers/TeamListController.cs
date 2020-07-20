using SitefinityWebApp.Mvc.Models.TeamList;
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
    [ControllerToolboxItem(Name = "TeamList", Title = "Team List", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class TeamListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TeamListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new TeamListModel();
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
            return View("TeamList." + this.template, this.Model.GetViewModel());
        }

        private TeamListModel model;
        private string template = "Default";
    }
}
