using SitefinityWebApp.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "ResponsiveEmbedIFrames", Title = "IFrame", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfMvcIcn")]
    public class ResponsiveEmbedIFramesController : Controller
    {
        public string ContentUrl { get; set; }

        public string Height { get; set; }
        public string Title { get; set; }
        public string ListTemplateName
        {
            get
            {
                return this.listTemplateName;
            }

            set
            {
                this.listTemplateName = value;
            }
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        /// <summary>
        /// Gets the full name of the list template.
        /// </summary>
        /// <returns></returns>
        private string GetFullListTemplateName()
        {
            return this.listTemplateNamePrefix + this.ListTemplateName;
        }


        private string listTemplateName = "default";

        private readonly string listTemplateNamePrefix = "iframe.";


        // GET: ResponsiveEmbedIFrames
        public ActionResult Index()
        {
            var responsiveEmbedIFramesModel = new ResponsiveEmbedIFramesModel
            {
                ContentUrl = ContentUrl,
                Height = Height,
                Title = Title
            };

            var fullTemplateName = this.GetFullListTemplateName();
            return View(fullTemplateName, responsiveEmbedIFramesModel);
        }
    }
}