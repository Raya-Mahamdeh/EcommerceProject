using EcommerceProject.API.Data;
using EcommerceProject.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace EcommerceProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //------------------------------ GetAll ---------------------------------------
        [HttpGet("")]
        public IActionResult getAll()
        {

            var categories = _context.Brands.ToList();

            return Ok(categories);//200

        }
        //------------------------------ GetByID ---------------------------------------
        [HttpGet("{id}")]//("{id:int}")
        public IActionResult getById([FromRoute] int id)
        {

            var brands = _context.Brands.Find(id);
            return brands == null ? NotFound() : Ok(brands);


        }
        //------------------------------- Create ---------------------------------------------
        [HttpPost("")]
        public IActionResult createBrand([FromBody] Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            //return Created();
            //return Created ($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}",category)
            return CreatedAtAction(nameof(getById), new { brand.Id }, brand);


        }
        //--------------------------------- Update -------------------------------------------
        [HttpPut("{id}")]
        public IActionResult updateBrand([FromRoute] int id, [FromBody] Brand brand)
        {
            var brandInDb = _context.Brands.AsNoTracking().FirstOrDefault(C => C.Id == id);
            if (brandInDb == null) return NotFound();
            brand.Id = brandInDb.Id;
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return NoContent();//200
        }
        //-------------------------------- Delete -------------------------------------------
        [HttpDelete("{id}")]
        public IActionResult deleteBrand([FromRoute] int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand is null)
            {
                return NotFound();//404
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
