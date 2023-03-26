using Microsoft.AspNetCore.Mvc;
using SportStoreFreeman.Repositories.Db;

namespace SportStoreFreeman.Views.Shared.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository _storeRepository;

        public NavigationMenuViewComponent(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_storeRepository.Products
                .Select(x=> x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
