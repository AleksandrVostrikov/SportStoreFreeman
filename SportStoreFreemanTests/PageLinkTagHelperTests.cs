using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportStoreFreeman.Infrastructure;
using SportStoreFreeman.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreFreemanTests
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void CanGeneratePageLink()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f =>
            f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);
            PageLinkTagHelper helper = new(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };
            TagHelperContext tagHelperContext = new(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");
            var content = new Mock<TagHelperContent>();
            TagHelperOutput tagHelperOutput = new("div",
                new TagHelperAttributeList(),
                (cache, encoder)=>Task.FromResult(content.Object));
            helper.Process(tagHelperContext, tagHelperOutput);

            Assert.Equal(@"<a href=""Test/Page1"">1</a>" + @"<a href=""Test/Page2"">2</a>" + @"<a href=""Test/Page3"">3</a>",
                tagHelperOutput.Content.GetContent());
        }
    }
}
