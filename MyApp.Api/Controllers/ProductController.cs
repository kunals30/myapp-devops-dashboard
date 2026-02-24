using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 50000 },
        new Product { Id = 2, Name = "Mobile", Price = 20000 }
    };

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_products);
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return Ok(product);
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
