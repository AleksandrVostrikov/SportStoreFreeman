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

            ProductListViewModel result = homeController.Index(null).ViewData.Model as ProductListViewModel;

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

            ProductListViewModel result = homeController.Index(null, 2).ViewData.Model as ProductListViewModel;

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
                homeController.Index(null, 2).ViewData.Model as ProductListViewModel;

            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
        
        [Fact]
        public void CanFilterProducts()
        {
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ ProductId = Guid.NewGuid(), Name = "N1", Category = "Cat1"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N2", Category = "Cat2"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N3", Category = "Cat1"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N4", Category = "Cat2"},
                new Product{ ProductId = Guid.NewGuid(), Name = "N5", Category = "Cat3"}
            }).AsQueryable());

            HomeController homeController = new(mock.Object);
            homeController.PageSize = 3;

            Product[] result =
                (homeController.Index("Cat2", 1).ViewData.Model as ProductListViewModel).Products.ToArray();

            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "N2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "N4" && result[1].Category == "Cat2");
        }
    }
}