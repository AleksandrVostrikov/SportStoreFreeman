using Microsoft.AspNetCore.Mvc;
using SportStoreFreeman.Models;
using SportStoreFreeman.Models.ViewModels;
using SportStoreFreeman.Repositories.Db;
using System.Diagnostics;

namespace SportStoreFreeman.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        public int PageSize = 4;

        public HomeController(
            IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public ViewResult Index(string category,int productPage = 1)
        {
            return View(new ProductListViewModel
            {
                Products = _storeRepository.Products
                .Where(p => p.Category == category || category == null)
                .OrderBy(p => p.Name)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new()
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? 
                    _storeRepository.Products.Count() : _storeRepository.Products
                    .Where(p => p.Category == category).Count()
                },
                CurrentCategory = category
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}