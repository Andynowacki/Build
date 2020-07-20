using SitefinityWebApp.Mvc.Models;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "TextImageSplit", Title = "Text Image Split", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class TextImageSplitController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TextImageSplitModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new TextImageSplitModel();
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
            return View("TextImageSplit." + this.template, this.Model.GetViewModel());
        }

        private TextImageSplitModel model;
        private string template = "Default";
    }
}