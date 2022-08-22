using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.DTO.CategoryDTO;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController:Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public CategoryController(AppDbContext context, IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOne(int id)
        {
            Category category = _context.Categories.Include(p=>p.Products).FirstOrDefault(p => p.Id == id&&!p.IsDeleted);
            /*if (category  == null) return NotFound();
            CategoryReturnDto categoryReturnDto = new CategoryReturnDto();
            categoryReturnDto.Name = category.Name;
            categoryReturnDto.ImageUrl = "http://localhost:3317/img"+ category.ImageUrl;*/
            CategoryReturnDto categoryReturnDto = _mapper.Map<CategoryReturnDto>(category);
            return Ok(categoryReturnDto);
        }

        [HttpGet]
        //[Route("All")]
        public IActionResult GetAll() 
        {
            var query = _context.Categories.Where(c=>!c.IsDeleted).AsQueryable();

            List<CategoryReturnDto> categoryReturnDtos = _mapper.Map < List<CategoryReturnDto>>(query.ToList());

            CategoryListDto categoryListDto = _mapper.Map < CategoryListDto > (categoryReturnDtos);
            return StatusCode(200, categoryListDto);
        }

        [HttpPost]
        public IActionResult Create([FromForm]CategoryCreateDto categoryCreateDto)
        {
            bool existCategory = _context.Categories.Any(c=>c.Name.ToLower()==categoryCreateDto.Name.ToLower());
            if (existCategory)
            {
                return StatusCode(409);
            }
            if (categoryCreateDto.Photo == null)
            {
                return BadRequest();
            }

            if (!categoryCreateDto.Photo.IsImage())
            {
                return BadRequest();
            }
            if (categoryCreateDto.Photo.ValidSize(10000))
            {
                ModelState.AddModelError("Photo", "Image size can not be large");
                return View();
            }
            Category category = new Category();
            category.Name = categoryCreateDto.Name;
            category.CreatedTime = DateTime.Now;
            category.ImageUrl = categoryCreateDto.Photo.SaveImage(_env, "img");
            _context.Categories.Add(category);
            _context.SaveChanges();
            return StatusCode(201);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id,[FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            Category c = _context.Categories.FirstOrDefault(c => c.Id == Id&&!c.IsDeleted);
            if (c == null) return NotFound();
            if (_context.Categories.Any(c=>c.Name.ToLower()==categoryUpdateDto.Name.ToLower()&&c.Id!=Id))
            {
                return BadRequest();
            }
            if (categoryUpdateDto!=null)
            {
                if (!categoryUpdateDto.Photo.IsImage())
                {
                    return StatusCode(200, "only images");
                }
                if (categoryUpdateDto.Photo.ValidSize(10000))
                {
                    return StatusCode(200, "image size should be lower than 10mb");
                }

                string oldImg = c.ImageUrl;
                string path = Path.Combine(_env.WebRootPath, "img", oldImg);
                Helper.Helper.DeleteImage(path);
                c.ImageUrl = categoryUpdateDto.Photo.SaveImage(_env, "img");
            }
            c.Name = categoryUpdateDto.Name;
            
            await _context.SaveChangesAsync();
            return StatusCode(200, "updated");
        }
    }
}
