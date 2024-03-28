using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomApiNetCore.Contexts;
using CustomApiNetCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CustomApiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CategoryController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategory([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "")
        {
            if (_context.Category == null)
            {
                return NotFound();
            }

            var getData = _context.Category.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                getData = getData.Where(x => x.name.Contains(filter));
            }

            var totalItems = getData.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            getData = getData.OrderBy(x => x.name);
            var items = await getData.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            if (items == null || items.Count == 0)
            {
                return Ok(new { success = false, message = "No data found." });
            }
            else
            {
                return Ok(new { success = true, TotalCount = totalItems, TotalPages = totalPages, CurrentPage = page, PageSize = pageSize, data = items });
            }
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryModel(long id)
        {
          if (_context.Category == null)
          {
              return NotFound();
          }
            var categoryModel = await _context.Category.FindAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return categoryModel;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryModel(long id, CategoryModel categoryModel)
        {
            if (id != categoryModel.id)
            {
                return BadRequest();
            }

            _context.Entry(categoryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategoryModel(CategoryModel categoryModel)
        {
          if (_context.Category == null)
          {
              return Problem("Entity set 'DatabaseContext.Category'  is null.");
          }
            _context.Category.Add(categoryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryModel", new { id = categoryModel.id }, categoryModel);
        }

        // DELETE: api/Category/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCategoryModel(long id)
        //{
        //    if (_context.Category == null)
        //    {
        //        return NotFound();
        //    }
        //    var categoryModel = await _context.Category.FindAsync(id);
        //    if (categoryModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Category.Remove(categoryModel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusCategoryModel(long id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var data = await _context.Category.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            data.is_deleted = 1;
            _context.Category.Update(data);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool CategoryModelExists(long id)
        {
            return (_context.Category?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
