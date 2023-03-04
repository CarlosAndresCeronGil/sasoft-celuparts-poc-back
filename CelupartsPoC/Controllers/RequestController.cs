using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet("Retomas/{page}/RequestDate")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetRetomasByDate(int page, [FromQuery] DateTime InitialDate, [FromQuery] DateTime FinalDate, [FromQuery] string? RequestStatus, [FromQuery] string? UserDtoIdNumber, [FromQuery] string? UserDtoName, [FromQuery] string? UserDtoSurname, [FromQuery] string? EquipmentBrand, [FromQuery] string? EquipmentModel)
        {
            var pageResults = 10f;

            if (FinalDate == DateTime.MinValue) {

                //var pageCountWithoutFinalDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Retoma").Where((x => x.RequestDate >= InitialDate)).Count() / pageResults);

                var requestsWithoutFinalDate = await _context.Request.AsNoTracking().Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate >= InitialDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                //Filtros para estado de retoma------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de retoma-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                //Filtros para modelo de equipo----------------------
                .Where(x => EquipmentModel != null ? x.Equipment!.ModelOrReference.Contains(EquipmentModel) : x.Equipment!.ModelOrReference == x.Equipment.ModelOrReference)
                //Fin de filtros para modelo de equipo---------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

                var finalRequestWithoutFinalDate = requestsWithoutFinalDate.Take((int)pageResults).ToList();

                var pageCountWithoutFinalDate = Math.Ceiling(requestsWithoutFinalDate.Count() / pageResults);

                var responseWithoutFinalDate = new RequestResponse
                {
                    Requests = finalRequestWithoutFinalDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutFinalDate
                };

                return Ok(responseWithoutFinalDate);

            }
            else if (InitialDate == DateTime.MinValue)
            {

                //var pageCountWithoutInitialDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Retoma").Where((x => x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

                var requestsWithoutInitialDate = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate <= FinalDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                //Filtros para estado de retoma------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de retoma-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                //Filtros para modelo de equipo----------------------
                .Where(x => EquipmentModel != null ? x.Equipment!.ModelOrReference.Contains(EquipmentModel) : x.Equipment!.ModelOrReference == x.Equipment.ModelOrReference)
                //Fin de filtros para modelo de equipo---------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

                var finalRequestWithoutInitialDate = requestsWithoutInitialDate.Take((int)pageResults).ToList();

                var pageCountWithoutInitialDate = Math.Ceiling(requestsWithoutInitialDate.Count() / pageResults);

                var responseWithoutInitialDate = new RequestResponse
                {
                    Requests = finalRequestWithoutInitialDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutInitialDate
                };

                return Ok(responseWithoutInitialDate);
            }

            //var pageCount = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Retoma").Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

            var requests = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                //Filtros para estado de retoma------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de retoma-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

            var finalRequest = requests.Take((int)pageResults).ToList();

            var pageCount = Math.Ceiling(requests.Count() / pageResults);

            var response = new RequestResponse
            {
                Requests = finalRequest,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet("Repairs/{page}/RequestDate")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetRepairsByDate(int page, [FromQuery] DateTime InitialDate, [FromQuery] DateTime FinalDate, [FromQuery] string? RequestStatus, [FromQuery] string? UserDtoIdNumber, [FromQuery] string? UserDtoName, [FromQuery] string? UserDtoSurname, [FromQuery] string? EquipmentBrand, [FromQuery] string? EquipmentModel)
        {
            var pageResults = 10f;

            if (FinalDate == DateTime.MinValue)
            {

                //var pageCountWithoutFinalDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate >= InitialDate)).Count() / pageResults);

                var requestsWithoutFinalDate = await _context.Request.AsNoTracking().Where(req => req.RequestType == "Reparacion")
                .Where((x => x.RequestDate >= InitialDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.Technician)
                //Filtros para estado de reparación------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de reparación-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                //Filtros para modelo de equipo----------------------
                .Where(x => EquipmentModel != null ? x.Equipment!.ModelOrReference.Contains(EquipmentModel) : x.Equipment!.ModelOrReference == x.Equipment.ModelOrReference)
                //Fin de filtros para modelo de equipo---------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

                var finalRequestWithoutFinalDate = requestsWithoutFinalDate.Take((int)pageResults).ToList();

                var pageCountWithoutFinalDate = Math.Ceiling(requestsWithoutFinalDate.Count() / pageResults);

                var responseWithoutFinalDate = new RequestResponse
                {
                    Requests = finalRequestWithoutFinalDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutFinalDate
                };

                return Ok(responseWithoutFinalDate);

            }
            else if (InitialDate == DateTime.MinValue)
            {

                //var pageCountWithoutInitialDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

                var requestsWithoutInitialDate = await _context.Request.Where(req => req.RequestType == "Reparacion")
                .Where((x => x.RequestDate <= FinalDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                //Filtros para estado de reparación------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de reparación-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                //Filtros para modelo de equipo----------------------
                .Where(x => EquipmentModel != null ? x.Equipment!.ModelOrReference.Contains(EquipmentModel) : x.Equipment!.ModelOrReference == x.Equipment.ModelOrReference)
                //Fin de filtros para modelo de equipo---------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

                var finalRequestWithoutInitialDate = requestsWithoutInitialDate.Take((int)pageResults).ToList();

                var pageCountWithoutFinalDate = Math.Ceiling(requestsWithoutInitialDate.Count() / pageResults);

                var responseWithoutInitialDate = new RequestResponse
                {
                    Requests = finalRequestWithoutInitialDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutFinalDate
                };

                return Ok(responseWithoutInitialDate);
            }

            //var pageCount = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

            var requests = await _context.Request.Where(req => req.RequestType == "Reparacion")
                .Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                //Filtros para estado de reparación------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de reparación-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

            var finalRequest = requests.Take((int)pageResults).ToList();

            var pageCount = Math.Ceiling(requests.Count() / pageResults);

            var response = new RequestResponse
            {
                Requests = finalRequest,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet("AllRequests/{page}/RequestDate")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetAllRequestsByDate(int page, [FromQuery] DateTime InitialDate, [FromQuery] DateTime FinalDate, [FromQuery] string? RequestStatus, [FromQuery] string? UserDtoIdNumber, [FromQuery] string? UserDtoName, [FromQuery] string? UserDtoSurname, [FromQuery] string? EquipmentBrand, [FromQuery] string? EquipmentModel)
        {
            var pageResults = 10f;

            if (FinalDate == DateTime.MinValue)
            {
                //var pageCountWithoutFinalDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate >= InitialDate)).Count() / pageResults);

                var requestsWithoutFinalDate = await _context.Request.AsNoTracking()
                .Where((x => x.RequestDate >= InitialDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.Technician)
                //Filtros para estado de reparación------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de reparación-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                //Filtros para modelo de equipo----------------------
                .Where(x => EquipmentModel != null ? x.Equipment!.ModelOrReference.Contains(EquipmentModel) : x.Equipment!.ModelOrReference == x.Equipment.ModelOrReference)
                //Fin de filtros para modelo de equipo---------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

                var finalRequestWithoutFinalDate = requestsWithoutFinalDate.Take((int)pageResults).ToList();

                var pageCountWithoutFinalDate = Math.Ceiling(requestsWithoutFinalDate.Count() / pageResults);

                var responseWithoutFinalDate = new RequestResponse
                {
                    Requests = finalRequestWithoutFinalDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutFinalDate
                };

                return Ok(responseWithoutFinalDate);

            }
            else if (InitialDate == DateTime.MinValue)
            {

                //var pageCountWithoutInitialDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

                var requestsWithoutInitialDate = await _context.Request
                .Where((x => x.RequestDate <= FinalDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                //Filtros para estado de reparación------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de reparación-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                //Filtros para modelo de equipo----------------------
                .Where(x => EquipmentModel != null ? x.Equipment!.ModelOrReference.Contains(EquipmentModel) : x.Equipment!.ModelOrReference == x.Equipment.ModelOrReference)
                //Fin de filtros para modelo de equipo---------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

                var finalRequestWithoutInitialDate = requestsWithoutInitialDate.Take((int)pageResults).ToList();

                var pageCountWithoutFinalDate = Math.Ceiling(requestsWithoutInitialDate.Count() / pageResults);

                var responseWithoutInitialDate = new RequestResponse
                {
                    Requests = finalRequestWithoutInitialDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutFinalDate
                };

                return Ok(responseWithoutInitialDate);
            }

            //var pageCount = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

            var requests = await _context.Request
                .Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate))
                //Filtros para documento de cliente-----------------
                .Where(x => UserDtoIdNumber != null ? x.UserDto!.IdNumber.Equals(UserDtoIdNumber) : x.UserDto!.IdNumber == x.UserDto.IdNumber)
                .Include(x => x.UserDto)
                //Fin de filtros para documento de cliente----------
                //Filtros para nombre del cliente-------------------
                .Where(x => UserDtoName != null ? x.UserDto!.Names.Contains(UserDtoName) : x.UserDto!.Names == x.UserDto.Names)
                .Where(x => UserDtoSurname != null ? x.UserDto!.Surnames.Contains(UserDtoSurname) : x.UserDto!.Surnames == x.UserDto.Surnames)
                //Fin de filtros para nombre del cliente------------
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                //Filtros para estado de reparación------------------
                .Where(x => x.RequestStatus.Any(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                .Include(x => x.RequestStatus.Where(x => RequestStatus != null ? x.Status == RequestStatus : x.Status.Contains("")))
                //Fin de filtros para estado de reparación-----------
                .Include(x => x.HomeServices)
                //Filtros para marca de equipo-----------------------
                .Where(x => EquipmentBrand != null ? x.Equipment!.EquipmentBrand.Contains(EquipmentBrand) : x.Equipment!.EquipmentBrand == x.Equipment.EquipmentBrand)
                .Include(x => x.Equipment)
                //Fin de filtros para marca de equipo----------------
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                //.Take((int)pageResults)
                .ToListAsync();

            var finalRequest = requests.Take((int)pageResults).ToList();

            var pageCount = Math.Ceiling(requests.Count() / pageResults);

            var response = new RequestResponse
            {
                Requests = finalRequest,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetAll()
        {
            var requests = _context.Request
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications);
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestWithEquipments>> Get(int id)
        {
            //var request = _context.Request.FindAsync(id);
            var request = _context.Request.Where(n => n.IdRequest == id)
                .Include(x => x.HomeServices)
                .Include(x => x.RequestStatus);
            if(request == null)
            {
                return BadRequest("Request not found!");
            }
            return Ok(request);
        }

        [HttpGet("UserInfo/{id}")]
        public async Task<ActionResult<RequestWithEquipments>> GetWithUserInfo(int id)
        {
            //var request = _context.Request.FindAsync(id);
            var request = _context.Request.Where(n => n.IdRequest == id)
                .Include(x => x.UserDto);
            if (request == null)
            {
                return BadRequest("Request not found!");
            }
            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<List<RequestWithEquipments>>> AddUser(RequestWithEquipments request)
        {
            //request.RequestDate = DateTime.UtcNow.Date; Devuelve 2022-09-05 00:00:00.0000000
            //request.RequestDate = DateTime.Today; Devuelve 2022-09-05 00:00:00.0000000
            request.RequestDate = DateTime.Now;
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return Ok(await _context.Request.FindAsync(request.IdRequest));
        }

        [HttpPut]
        public async Task<ActionResult<List<RequestWithEquipments>>> UpdateRequest(RequestWithEquipments requestR)
        {
            var dbRequest = _context.Request.FindAsync(requestR.IdRequest);
            if (dbRequest.Result == null)
            {
                return BadRequest("Request not found!");
            }
            dbRequest.Result.IdUser = requestR.IdUser;
            dbRequest.Result.IdEquipment = requestR.IdEquipment;
            dbRequest.Result.RequestType = requestR.RequestType;
            dbRequest.Result.PickUpAddress = requestR.PickUpAddress;
            dbRequest.Result.DeliveryAddress = requestR.DeliveryAddress;
            dbRequest.Result.StatusQuote = requestR.StatusQuote;
            dbRequest.Result.AutoDiagnosis = requestR.AutoDiagnosis;

            await _context.SaveChangesAsync();

            return Ok(await _context.Request.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RequestWithEquipments>>> Delete(int id)
        {
            var dbRequest = _context.Request.FindAsync(id);
            if (dbRequest.Result == null)
            {
                return BadRequest("Request not found!");
            }
            _context.Request.Remove(dbRequest.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Request.ToListAsync());
        }
    }
}
