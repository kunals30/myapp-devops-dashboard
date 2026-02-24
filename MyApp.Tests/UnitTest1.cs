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
        public void Get_ReturnsEmptyList_Initially()
        {
            var controller = new ProductController();

            var result = controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.Empty(products);
        }

        [Fact]
        public void Add_ValidProduct_ReturnsOk()
        {
            var controller = new ProductController();

            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 100
            };

            var result = controller.Add(product);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal("Test Product", returnedProduct.Name);
        }

        [Fact]
        public void Add_NullProduct_ReturnsBadRequest()
        {
            var controller = new ProductController();

            var result = controller.Add(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Add_Then_Get_ShouldContainProduct()
        {
            var controller = new ProductController();

            var product = new Product
            {
                Id = 2,
                Name = "Laptop",
                Price = 500
            };

            controller.Add(product);

            var result = controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.Contains(products, p => p.Id == 2);
        }
    }

    public class HealthControllerTests
    {
        [Fact]
        public void Get_ReturnsHealthyStatus()
        {
            var controller = new HealthController();

            var result = controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Healthy", okResult.Value);
        }
    }
}
