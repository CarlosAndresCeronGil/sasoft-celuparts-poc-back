using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductReviewController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductReview>>> Get()
        {
            return Ok(await _context.ProductReview.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReview>> Get(int id)
        {
            var productReview = _context.ProductReview.FindAsync(id);
            if (productReview.Result == null)
            {
                return BadRequest("Product review not found!");
            }
            return Ok(productReview.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductReview>>> AddProductReview(ProductReview productReview)
        {
            _context.ProductReview.Add(productReview);
            await _context.SaveChangesAsync();

            return Ok(await _context.ProductReview.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ProductReview>>> UpdateProductReview(ProductReview request)
        {
            var dbProductReview = _context.ProductReview.FindAsync(request.IdProductReview);
            if (dbProductReview.Result == null)
            {
                return BadRequest("Product review not found!");
            }
            dbProductReview.Result.IdRequest = request.IdRequest;
            dbProductReview.Result.RepairDate = request.RepairDate;
            dbProductReview.Result.TechnicalRemarks = request.TechnicalRemarks;

            await _context.SaveChangesAsync();

            return Ok(await _context.ProductReview.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductReview>>> Delete(int id)
        {
            var dbProductReview = _context.ProductReview.FindAsync(id);
            if (dbProductReview.Result == null)
            {
                return BadRequest("Product review not found!");
            }
            _context.ProductReview.Remove(dbProductReview.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.ProductReview.ToListAsync());
        }
    }
}
