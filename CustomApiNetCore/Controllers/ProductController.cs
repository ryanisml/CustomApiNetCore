using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomApiNetCore.Contexts;
using CustomApiNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CustomApiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _context;  

        public ProductController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProduct([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string filter = "")
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var getData = _context.Product.AsQueryable();
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

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(long id)
        {
          if (_context.Product == null)
          {
              return NotFound();
          }
            var productModel = await _context.Product.FindAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return productModel;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(long id, ProductModel productModel)
        {
            if (id != productModel.id)
            {
                return BadRequest();
            }

            _context.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductModel(ProductModel productModel)
        {
          if (_context.Product == null)
          {
              return Problem("Entity set 'DatabaseContext.Product'  is null.");
          }
            _context.Product.Add(productModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductModel", new { id = productModel.id }, productModel);
        }

        // DELETE: api/Product/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProductModel(long id)
        //{
        //    if (_context.Product == null)
        //    {
        //        return NotFound();
        //    }
        //    var productModel = await _context.Product.FindAsync(id);
        //    if (productModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Product.Remove(productModel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // UPDATE delete status: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusProductModel(long id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var productModel = await _context.Product.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }

            productModel.is_deleted = 1;
            _context.Product.Update(productModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ProductModelExists(long id)
        {
            return (_context.Product?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
