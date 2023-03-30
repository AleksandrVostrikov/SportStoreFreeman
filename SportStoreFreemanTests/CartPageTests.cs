using Moq;
using SportStoreFreeman.Models;
using SportStoreFreeman.Pages;
using SportStoreFreeman.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreFreemanTests
{
    public class CartPageTests
    {
        [Fact]
        public void CanLoadCArt()
        {
            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "N1"};
            Product p2 = new Product { ProductId = Guid.NewGuid(), Name = "N2" };
            Mock<IStoreRepository> mockRepo = new();
            mockRepo.Setup(m => m.Products).Returns((new Product[]
            {
                p1, p2
            }).AsQueryable());

            Cart testCart = new();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            CartModel cartModel = new(mockRepo.Object, testCart);
            cartModel.OnGet("myUrl");

            Assert.Equal(2, cartModel.Cart.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        [Fact]
        public void CanUpdateCart()
        {
            Product p1 = new Product { ProductId = Guid.NewGuid(), Name = "N1" };
            Mock<IStoreRepository> mockRepo = new();
            mockRepo.Setup(m => m.Products).Returns((new Product[]
            {
                p1
            }).AsQueryable());
            Cart testCart = new();

            CartModel cartModel = new(mockRepo.Object, testCart);
            cartModel.OnPost(p1.ProductId, "myUrl");

            Assert.Single(testCart.Lines);
            Assert.Equal("N1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Quantity);
        }
    }
}
