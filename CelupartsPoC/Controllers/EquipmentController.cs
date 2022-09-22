﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly DataContext _context;

        public EquipmentController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Equipment>>> Get()
        {
            var equipments = _context.Equipment.OrderByDescending(x => x.IdEquipment).ToList();
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> Get(int id)
        {
            var equipment = _context.Equipment.FindAsync(id);
            if (equipment.Result == null)
            {
                return BadRequest("Equipment not found!");
            }
            return Ok(equipment.Result);
        }

        [HttpGet("VerifyImei/{id}")]
        public async Task<ActionResult<string>> VerifyImei(string id)
        {
            Random random = new Random();
            int probabilidad = random.Next(1, 10);

            if(probabilidad == 1)
            {
                var respuestaRobado = "Dispositivo registrado como robado";
                return (JsonConvert.SerializeObject(respuestaRobado));
            }
            var respuesta = "Dispositivo válido";
            return Ok(JsonConvert.SerializeObject(respuesta));
        }

        /*[HttpPost]
        public async Task<ActionResult<List<Equipment>>> AddEquipment(Equipment equipment)
        {
            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Equipment.FindAsync(equipment.IdEquipment));
        }*/

        [HttpGet("downloadFile/{id}")]
        public async Task<ActionResult> Download(int id)
        {
            using(var context = _context)
            {
                var equipment = await context.Equipment.FindAsync(id);
                return File(equipment.EquipmentInvoice, "application/pdf", fileDownloadName: "factura.pdf");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<List<Equipment>>> AddEquipmentWithFile([FromForm] UploadModel uploadModel)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var equipment = new Equipment();
                await uploadModel.EquipmentInvoice.CopyToAsync(ms);

                // Upload the file if less than 1 MB
                if (ms.Length < 1090000)
                {
                    equipment.TypeOfEquipment = uploadModel.TypeOfEquipment;
                    equipment.EquipmentBrand = uploadModel.EquipmentBrand;
                    equipment.ModelOrReference = uploadModel.ModelOrReference;
                    equipment.ImeiOrSerial = uploadModel.ImeiOrSerial;

                    equipment.EquipmentInvoice = ms.ToArray();

                    _context.Equipment.Add(equipment);

                    await _context.SaveChangesAsync();

                    return Ok(await _context.Equipment.FindAsync(equipment.IdEquipment));
                } else {
                    return BadRequest("The file is too large.");
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Equipment>>> UpdateEquipment(Equipment request)
        {
            var dbEquipment = _context.Equipment.FindAsync(request.IdEquipment);
            if(dbEquipment.Result == null)
            {
                return BadRequest("Equipment not found!");
            }
            dbEquipment.Result.TypeOfEquipment = request.TypeOfEquipment;
            dbEquipment.Result.EquipmentBrand = request.EquipmentBrand;
            dbEquipment.Result.ModelOrReference = request.ModelOrReference;
            dbEquipment.Result.ImeiOrSerial = request.ImeiOrSerial;
            //dbEquipment.Result.EquipmentInvoice = request.EquipmentInvoice;

            await _context.SaveChangesAsync();

            return Ok(await _context.Equipment.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Equipment>>> Delete(int id)
        {
            var dbEquipment = _context.Equipment.FindAsync(id);
            if (dbEquipment.Result == null)
            {
                return BadRequest("Equipment not found!");
            }
            _context.Equipment.Remove(dbEquipment.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Equipment.ToListAsync());
        }
    }
}
