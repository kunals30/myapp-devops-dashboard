using Xunit;
using MyApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.NotNull(products);
        }

        [Fact]
        public void Add_ValidProduct_IncreasesCount()
        {
            var controller = new ProductController();

            // Get initial count
            var initialResult = controller.Get();
            var initialOk = Assert.IsType<OkObjectResult>(initialResult);
            var initialList = ((IEnumerable<Product>)initialOk.Value).ToList();
            var initialCount = initialList.Count;

            var product = new Product
            {
                Id = 999,
                Name = "CI Test Product",
                Price = 123
            };

            var addResult = controller.Add(product);
            var addOk = Assert.IsType<OkObjectResult>(addResult);
            var returnedProduct = Assert.IsType<Product>(addOk.Value);

            Assert.Equal("CI Test Product", returnedProduct.Name);

            // Verify count increased
            var afterResult = controller.Get();
            var afterOk = Assert.IsType<OkObjectResult>(afterResult);
            var afterList = ((IEnumerable<Product>)afterOk.Value).ToList();

            Assert.Equal(initialCount + 1, afterList.Count);
        }

        [Fact]
        public void Add_NullProduct_ReturnsBadRequest()
        {
            var controller = new ProductController();

            var result = controller.Add(null);

            Assert.IsType<BadRequestResult>(result);
        }
    }

    public class HealthControllerTests
    {
        [Fact]
        public void Get_ReturnsHealthyStatusObject()
        {
            var controller = new HealthController();

            var result = controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);

            // Anonymous object handling
            dynamic value = okResult.Value;

            Assert.Equal("Healthy", (string)value.Status);
            Assert.NotNull(value.Server);
            Assert.NotNull(value.Time);
        }
    }
}
