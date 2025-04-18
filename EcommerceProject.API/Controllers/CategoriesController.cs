﻿using EcommerceProject.API.Data;
using EcommerceProject.API.DTOs;
using EcommerceProject.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet("")]
        public IActionResult getAll()
        {

            var categories = _context.Categories.ToList();

            return Ok(categories);//200

        }
        //------------------------------ DetByID ---------------------------------------
        [HttpGet("{id}")]//("{id:int}")
        public IActionResult getById([FromRoute] int id)
        {

            var categories = _context.Categories.Find(id);
            return categories == null ? NotFound() : Ok(categories);


        }
        //------------------------------- Create ---------------------------------------------
        [HttpPost("")]
        public IActionResult createCategory([FromBody] Category category)
        {

            /* Category category = new Category()
             {
                 Name = categorydto.Name
             };*/
            _context.Categories.Add(category);
            _context.SaveChanges();
            //return Created();
            //return Created ($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category)
            return CreatedAtAction(nameof(getById), new { category.Id }, category);


        }
        //--------------------------------- Update -------------------------------------------
        [HttpPut("{id}")]
        public IActionResult updateCategory( [FromRoute]int id,[FromBody] Category category)
        {
            var categoryInDb = _context.Categories.AsNoTracking().FirstOrDefault(C => C.Id == id);
            if (categoryInDb == null)  return NotFound();
            category.Id = categoryInDb.Id;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return NoContent();//200
        }


        //-------------------------------- Delete -------------------------------------------
        [HttpDelete  ("{id}")]
        public IActionResult deleteCategory([FromRoute]int id)
        {
          
                var category = _context.Categories.Find(id);
                if (category is null)
                {
                    return NotFound();//404
                }

                _context.Categories.Remove(category);
                _context.SaveChanges();
                return NoContent();
            }
         
        }

    }

