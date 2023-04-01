﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using SportStoreFreeman.Controllers;
using SportStoreFreeman.Migrations;
using SportStoreFreeman.Models;
using SportStoreFreeman.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreFreemanTests
{
    public class OrderControllerTests
    {
        [Fact]
        public void CannotCheckoutEmptyCart()
        {
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            Order order = new();
            OrderController target = new(mock.Object, cart);
            ViewResult result = target.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CannotInvalidShippingDetails()
        {
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            cart.AddItem(new Product(), 1);
            OrderController target = new(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            ViewResult result = target.Checkout(new Order()) as ViewResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CanCheckoutAndSubmitOrder()
        {
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            cart.AddItem(new Product(), 1);
            OrderController target = new(mock.Object, cart);

            RedirectToPageResult result = target.Checkout(new Order()) as RedirectToPageResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("/Completed", result.PageName);
        }
    }
}
