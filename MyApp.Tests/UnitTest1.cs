using Xunit;
using MyApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyApp.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Get_ReturnsProductList()
        {
            var controller = new ProductController();

            var result = controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);

            Assert.NotNull(products);
        }

        [Fact]
        public void Add_ReturnsCreatedProduct()
        {
            var controller = new ProductController();

            var product = new MyApp.Api.Controllers.Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 100
            };

            var result = controller.Add(product);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}
