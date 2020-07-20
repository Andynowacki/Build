using SitefinityWebApp.Mvc.Models;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Banner", Title = "Banner", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class BannerController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BannerModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new BannerModel();
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
            return View("Banner." + this.template, this.Model.GetViewModel());
        }

        private BannerModel model;
        private string template = "Default";
    }
}