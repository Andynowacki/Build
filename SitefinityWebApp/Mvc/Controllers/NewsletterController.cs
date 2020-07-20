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
    [ControllerToolboxItem(Name = "Newsletter", Title = "IFrame - Newsletter", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfMvcIcn")]
    public class NewsletterController : Controller
    {
        public string ContentUrl { get; set; }

        public string Height { get; set; }
        public string Title { get; set; }

        public string SubTitle { get; set; }
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

        private readonly string listTemplateNamePrefix = "Newsletter.";


        // GET: ResponsiveEmbedIFrames
        public ActionResult Index()
        {
            var newsletterModel = new NewsletterModel
            {
                ContentUrl = ContentUrl,
                Height = Height,
                Title = Title,
                SubTitle = SubTitle
            };

            var fullTemplateName = this.GetFullListTemplateName();
            return View(fullTemplateName, newsletterModel);
        }
    }
}