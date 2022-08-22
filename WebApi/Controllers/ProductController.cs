using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        /*public ProductController()
        {

        }*/
        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOne(int id)
        {
            Product product= _context.Products.Where(p=>p.IsActive).Include(c=>c.Category).FirstOrDefault(p => p.Id == id);
            if (product== null) return NotFound();
            ProductReturnDto productReturnDto = _mapper.Map<ProductReturnDto>(product);
            return Ok(productReturnDto);
        }
        [HttpGet]
        //[Route("All")]
        public IActionResult GetAll()
        {

            #region manual mapping and select mapping
            /*foreach (var item in products)
       {
           ProductReturnDto productReturnDto = new ProductReturnDto();
           productReturnDto.Name = item.Name;
           productReturnDto.Price = item.Price;
           productReturnDto.IsActive = item.IsActive;
           ProductList.Items.Add(productReturnDto);

       }*/
            /*     ProductList.Items = query.Select(p=> new ProductReturnDto
                 {
                     Name = p.Name,
                     Price = p.Price,
                     IsActive = p.IsActive
                 }).ToList();
                 ProductList.Total = query.Count();*/
            #endregion


            var query = _context.Products.Where(p => p.IsActive).Include(c=>c.Category).AsQueryable();

            List<ProductReturnDto> productReturnDtos = _mapper.Map<List<ProductReturnDto>>(query.ToList());
            ProductListDto ProductList = _mapper.Map<ProductListDto>(productReturnDtos);
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


 /*       public int Plus(int num1, int num2)
        {
            return num1 + num2;
        }*/
    }
}
