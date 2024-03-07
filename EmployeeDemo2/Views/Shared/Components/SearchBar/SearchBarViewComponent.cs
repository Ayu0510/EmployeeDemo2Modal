using Microsoft.AspNetCore.Mvc;

namespace EmployeeDemo.web.Views.Shared.Components.SearchBar
{
    public class SearchBarViewComponent : ViewComponent
    {
        public SearchBarViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(SPager SearchPager)
        {
            return View("Default",SearchPager);
        }
    }
}
