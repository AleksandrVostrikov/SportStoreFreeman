﻿using Microsoft.AspNetCore.Mvc;
using SportStoreFreeman.Models;

namespace SportStoreFreeman.Views.Shared.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart _cartService;
        public CartSummaryViewComponent(Cart cartService)
        {
            _cartService = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cartService);
        }
    }
}
