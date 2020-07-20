using SitefinityWebApp.Mvc.Models;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Button", Title = "Button", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class ButtonController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ButtonModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new ButtonModel();
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
            return View("Button." + this.template, this.Model.GetViewModel());
        }

        private ButtonModel model;
        private string template = "Default";
    }
}