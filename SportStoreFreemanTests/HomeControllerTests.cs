using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStoreFreeman.Controllers;
using SportStoreFreeman.Models;
using SportStoreFreeman.Models.ViewModels;
using SportStoreFreeman.Repositories.Db;

namespace SportStoreFreemanTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void CanUseRepository()
        {
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ ProductId = Guid.NewGuid(), Name = "N1"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N2"}
            }).AsQueryable());
            HomeController homeController = new HomeController(mock.Object);

            ProductListViewModel result = homeController.Index().ViewData.Model as ProductListViewModel;

            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("N1", prodArray[0].Name);
            Assert.Equal("N2", prodArray[1].Name);
        }
        
        [Fact]
        public void CanPaginate() 
        {
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ ProductId = Guid.NewGuid(), Name = "N1"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N2"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N3"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N4"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N5"},
            }).AsQueryable());
            HomeController homeController = new HomeController(mock.Object);
            homeController.PageSize = 3;

            ProductListViewModel result = homeController.Index(2).ViewData.Model as ProductListViewModel;

            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("N4", prodArray[0].Name);
            Assert.Equal("N5", prodArray[1].Name);
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ ProductId = Guid.NewGuid(), Name = "N1"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N2"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N3"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N4"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N5"},
            }).AsQueryable());
            HomeController homeController = new HomeController(mock.Object) { PageSize = 3};
            ProductListViewModel result =
                homeController.Index(2).ViewData.Model as ProductListViewModel;

            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
    }
}