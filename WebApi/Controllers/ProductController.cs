using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOne(int id)
        {
            Product prdct = _context.Products.Where(p=>p.IsActive).FirstOrDefault(p => p.Id == id);
            if (prdct == null) return NotFound();
            return Ok(prdct);
        }
        [HttpGet]
        //[Route("All")]
        public IActionResult GetAll()
        {
            List<Product> products = _context.Products.ToList();
            return StatusCode(200, products.Where(p=>p.IsActive).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, Product product)
        {
            Product p = _context.Products.FirstOrDefault(pr => pr.Id == Id);
            if (p == null) return NotFound();
            p.Name=product.Name;
            p.Price=product.Price;
            p.IsActive = product.IsActive;
            await _context.SaveChangesAsync();
            return StatusCode(204, p.Name);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(pr => pr.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }
    }
}
