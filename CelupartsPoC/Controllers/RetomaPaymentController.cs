using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetomaPaymentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;
        public RetomaPaymentController(DataContext context, IWebHostEnvironment env)
        {
            this._context = context;
            _environment = env;
        }

        [HttpGet]
        public async Task<ActionResult<List<RetomaPayment>>> Get()
        {
            return Ok(await _context.RetomaPayment.Where(x => x.PaymentDate != null).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RetomaPayment>> Get(int id)
        {
            var retomaPayment = _context.RetomaPayment.FindAsync(id);
            if (retomaPayment.Result == null)
            {
                return BadRequest("Retoma payment not found!");
            }
            return Ok(retomaPayment.Result);
        }

        [HttpGet("downloadFile/{id}")]
        public async Task<ActionResult> Download(int id)
        {
            using (var context = _context)
            {
                var retomaPayment = await context.RetomaPayment.FindAsync(id);

                var fullFileName = System.IO.Path.Combine(_environment.ContentRootPath, "uploads",
                    retomaPayment!.BillPaymentPath);

                using (var fs = new System.IO.FileStream(fullFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
                {
                    using(var ms = new System.IO.MemoryStream())
                    {
                        await fs.CopyToAsync(ms);
                        var extension = Path.GetExtension(retomaPayment.BillPaymentPath).Substring(1);
                        return File(ms.ToArray(), extension == "pdf" ? "application/pdf" : "image/" + extension, fileDownloadName: "factura.pdf");
                    }
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<RetomaPayment>>> AddRetomaPayment(RetomaPayment retomaPayment)
        {
            _context.RetomaPayment.Add(retomaPayment);
            await _context.SaveChangesAsync();

            return Ok(await _context.RetomaPayment.ToListAsync());
        }

        /*[HttpPut]
        public async Task<ActionResult<List<RetomaPayment>>> UpdateRetomaPayment(RetomaPayment request)
        {
            var dbRetomaPayment = _context.RetomaPayment.FindAsync(request.IdRetomaPayment);
            if (dbRetomaPayment.Result == null)
            {
                return BadRequest("Retoma payment not found!");
            }
            dbRetomaPayment.Result.IdRetoma = request.IdRetoma;
            dbRetomaPayment.Result.PaymentMethod = request.PaymentMethod;
            dbRetomaPayment.Result.PaymentDate = request.PaymentDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.RetomaPayment.ToListAsync());
        }*/

        [HttpPut]
        public async Task<ActionResult> Upload([FromForm] RetomaPaymentUploadModel uploadModel)
        {
            try
            {
                var dbRetomaPayment = _context.RetomaPayment.FindAsync(uploadModel.IdRetomaPayment);
                if (dbRetomaPayment.Result == null)
                {
                    return BadRequest("Retoma payment not found!");
                }
                if (uploadModel.RetomaBillPayment != null)
                {
                    var fileName = System.IO.Path.Combine(_environment.ContentRootPath,
                            "uploads", uploadModel.VoucherNumber + "" + uploadModel.RetomaBillPayment.FileName);

                    await uploadModel.RetomaBillPayment.CopyToAsync(
                        new System.IO.FileStream(fileName, System.IO.FileMode.Create));

                    using (var context = _context)
                    {
                        //var retomaPayment = new RetomaPayment();
                        var retomaPayment = _context.RetomaPayment.FindAsync(uploadModel.IdRetomaPayment);

                        retomaPayment.Result!.IdRetomaPayment = uploadModel.IdRetomaPayment;
                        retomaPayment.Result.PaymentMethod = uploadModel.PaymentMethod;
                        retomaPayment.Result.PaymentDate = uploadModel.PaymentDate;
                        retomaPayment.Result.VoucherNumber = uploadModel.VoucherNumber;
                        retomaPayment.Result.IdRetoma = uploadModel.IdRetoma;

                        retomaPayment.Result.BillPaymentPath = uploadModel.VoucherNumber + "" + uploadModel.RetomaBillPayment.FileName;

                        //_context.RetomaPayment.Add(retomaPayment);

                        await _context.SaveChangesAsync();
                        return Ok(await _context.RetomaPayment.FindAsync(retomaPayment.Result.IdRetomaPayment));
                    }
                }
                else
                {
                    using (var context = _context)
                    {
                        //var retomaPayment = new RetomaPayment();
                        var retomaPayment = _context.RetomaPayment.FindAsync(uploadModel.IdRetomaPayment);

                        retomaPayment.Result!.IdRetomaPayment = uploadModel.IdRetomaPayment;
                        retomaPayment.Result.PaymentMethod = uploadModel.PaymentMethod;
                        retomaPayment.Result.PaymentDate = uploadModel.PaymentDate;
                        retomaPayment.Result.VoucherNumber = uploadModel.VoucherNumber;
                        retomaPayment.Result.IdRetoma = uploadModel.IdRetoma;

                        //_context.RetomaPayment.Add(retomaPayment);

                        await _context.SaveChangesAsync();
                        return Ok(await _context.RetomaPayment.FindAsync(retomaPayment.Result.IdRetomaPayment));
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RetomaPayment>>> Delete(int id)
        {
            var dbRetomaPayment = _context.RetomaPayment.FindAsync(id);
            if (dbRetomaPayment.Result == null)
            {
                return BadRequest("Retoma payment not found!");
            }
            _context.RetomaPayment.Remove(dbRetomaPayment.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.RetomaPayment.ToListAsync());
        }
    }
}
