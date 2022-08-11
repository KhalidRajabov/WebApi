using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.DTO.Product_DTOs;
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
            Product product= _context.Products.Where(p=>p.IsActive).FirstOrDefault(p => p.Id == id);
            if (product== null) return NotFound();
            ProductReturnDto productReturnDto = new ProductReturnDto();
            productReturnDto.Name = product.Name;
            productReturnDto.Price= product.Price;
            productReturnDto.IsActive = product.IsActive;
            return Ok(productReturnDto);
        }
        [HttpGet]
        //[Route("All")]
        public IActionResult GetAll()
        {
            var query = _context.Products.Where(p => p.IsActive);
            
            ProductListDto ProductList = new ProductListDto();
            /*foreach (var item in products)
            {
                ProductReturnDto productReturnDto = new ProductReturnDto();
                productReturnDto.Name = item.Name;
                productReturnDto.Price = item.Price;
                productReturnDto.IsActive = item.IsActive;
                ProductList.Items.Add(productReturnDto);
                
            }*/
            ProductList.Items = query.Select(p=> new ProductReturnDto
            {
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive
            }).ToList();
            ProductList.Total = query.Count();

            return StatusCode(200, ProductList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            Product product = new Product
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                IsActive = productCreateDto.IsActive,
                CategoryId = productCreateDto.CategoryId
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, ProductUpdateDto productUpdateeDto)
        {
            Product p = _context.Products.FirstOrDefault(pr => pr.Id == Id);
            if (p == null) return NotFound();
            p.Name= productUpdateeDto.Name;
            p.Price= productUpdateeDto.Price;
            p.IsActive = productUpdateeDto.IsActive;
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
