using SitefinityWebApp.Mvc.Models.CardList;
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
    [ControllerToolboxItem(Name = "CardList", Title = "Card List", SectionName = ToolboxesConfig.ContentToolboxSectionName)]
    public class CardListController : Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CardListModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new CardListModel();
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
            return View("CardList." + this.template, this.Model.GetViewModel());
        }

        private CardListModel model;
        private string template = "Default";
    }
}
