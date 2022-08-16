using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetomaController : ControllerBase
    {
        private readonly DataContext _context;
        public RetomaController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Retoma>>> Get()
        {
            var retomas = _context.Retoma.ToList();
            return Ok(retomas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Retoma>> Get(int id)
        {
            var retoma = _context.Retoma.FindAsync(id);
            if (retoma.Result == null)
            {
                return BadRequest("Retoma not found!");
            }
            return Ok(retoma.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Retoma>>> AddRetoma(Retoma retoma)
        {
            _context.Retoma.Add(retoma);
            await _context.SaveChangesAsync();

            return Ok(await _context.Retoma.FindAsync(retoma.IdRetoma));
        }

        [HttpPut]
        public async Task<ActionResult<List<Retoma>>> UpdateRetoma(Retoma request)
        {
            var dbRetoma = _context.Retoma.FindAsync(request.IdRetoma);
            if (dbRetoma.Result == null)
            {
                return BadRequest("Retoma not found!");
            }
            dbRetoma.Result.IdRequest = request.IdRequest;
            dbRetoma.Result.RetomaQuote = request.RetomaQuote;
            dbRetoma.Result.DeviceDiagnostic = request.DeviceDiagnostic;

            await _context.SaveChangesAsync();

            return Ok(await _context.Retoma.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Retoma>>> Delete(int id)
        {
            var dbRetoma = _context.Retoma.FindAsync(id);
            if (dbRetoma.Result == null)
            {
                return BadRequest("Retoma not found!");
            }
            _context.Retoma.Remove(dbRetoma.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Retoma.ToListAsync());
        }
    }
}
