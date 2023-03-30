using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportStoreFreeman.Infrastructure;
using SportStoreFreeman.Models;
using SportStoreFreeman.Repositories.Db;

namespace SportStoreFreeman.Pages
{
    public class CartModel : PageModel
    {
        private readonly IStoreRepository _storeRepository;

        public CartModel(IStoreRepository storeRepository, Cart cartService)
        {
            _storeRepository = storeRepository;
            Cart = cartService;
        }
        
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(Guid productId, string returnUrl)
        {
            Product product = _storeRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            Cart.AddItem(product, 1);
            return RedirectToPage(new { returnUrl });
        }

        public IActionResult OnPostRemove(Guid productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
            cl.Product.ProductId == productId).Product);
            return RedirectToPage(new { returnUrl });
        }
    }
}
