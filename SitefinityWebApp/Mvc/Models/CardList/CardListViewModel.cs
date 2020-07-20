
using System.Collections.Generic;


namespace SitefinityWebApp.Mvc.Models.CardList
{
    public class CardListViewModel
    {
      

        public string Title { get; set; }
        public string Content { get; set; }
        public bool CenterText { get; set; }
        public List<Page> CardItems { get; set; }
    }
}
