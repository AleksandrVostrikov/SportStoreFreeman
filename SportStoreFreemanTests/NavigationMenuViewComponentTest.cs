using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NuGet.ContentModel;
using SportStoreFreeman.Models;
using SportStoreFreeman.Repositories.Db;
using SportStoreFreeman.Views.Shared.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreFreemanTests
{
    public class NavigationMenuViewComponentTest
    {
        [Fact]
        public void CanSelectCategories()
        {
            Mock<IStoreRepository> mock = new();
            mock.Setup(m=> m.Products).Returns(( new Product[]
            {
                new Product {ProductId = Guid.NewGuid(), Name = "N1", Category = "Apples"},
                new Product {ProductId = Guid.NewGuid(), Name = "N2", Category = "Apples"},
                new Product {ProductId = Guid.NewGuid(), Name = "N3", Category = "Plums"},
                new Product {ProductId = Guid.NewGuid(), Name = "N4", Category = "Oranges"},
            }).AsQueryable());
            NavigationMenuViewComponent target = new(mock.Object);

            string[] results = ((IEnumerable<string>)(target.Invoke() 
                as ViewViewComponentResult).ViewData.Model).ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }

        [Fact]
        public void IndicatesSelectedCategory() 
        {
            string categoryToSelect = "Apples";
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = Guid.NewGuid(), Name = "N1", Category = "Apples"},
                new Product {ProductId = Guid.NewGuid(), Name = "N1", Category = "Oranges"},
            }).AsQueryable());
            NavigationMenuViewComponent target = new(mock.Object);
            target.ViewComponentContext = new()
            {
                ViewContext = new()
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            string result = (string)(target.Invoke() as ViewViewComponentResult)
                .ViewData["SelectedCategory"];

            Assert.Equal(categoryToSelect, result);
        }
    }
}
