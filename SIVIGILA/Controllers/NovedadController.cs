using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.NovedadesDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.NovedadesService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NovedadController : ControllerBase
    {
        private readonly INovedadService _service;
        public NovedadController(INovedadService service)
        {
            _service = service;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> GetByParamsAsync([FromQuery] SearchNovedadesDTO Dto)
        {
            return Ok(await _service.GetByParamsAsync(Dto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(GetDefaultResponse(await _service.GetByIdAsync(id)));
        }
        [HttpGet("Exist/{id}")]
        public async Task<IActionResult> ExistByIdAsync(int id)
        {
            return Ok(GetDefaultResponse(await _service.ExistByIDAsync(id)));
        }
        [HttpGet("Exist/Name/{nombre}")]
        public async Task<IActionResult> ExistBynameAsync(string nombre)
        {
            return Ok(GetDefaultResponse(await _service.ExistByNameAsync(nombre)));
        }
        [HttpGet("Active/All")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(GetDefaultResponse(await _service.GetAllAsync()));
        }
        [HttpGet("Name/All")]
        public async Task<IActionResult> GetAllnamesAsync([FromQuery] string? search)
        {
            return Ok(GetDefaultResponse(await _service.GetAllNamesAsync(search)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(NovedadesDTO Dto)
        {
            //Se obtiene el AzureID del token
            Dto.ResponsableID = Guid.NewGuid();
            return Ok(GetDefaultResponse(await _service.AddAsync(Dto)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, NovedadesDTO Dto)
        {
            Dto.NovedadesID = id;
            //Se obtiene el AzureID del token
            Dto.ResponsableID = Guid.NewGuid();
            return Ok(GetDefaultResponse(await _service.UpdateAsync(Dto)));
        }
        [HttpPost("Range")]
        public async Task<IActionResult> CreateOrUpdateRangeAsync(IEnumerable<NovedadesDTO> lista)
        {
            var ResponsableID = Guid.NewGuid(); //Se obtiene del Token
            foreach (var dto in lista)
                dto.ResponsableID = ResponsableID;
            return Ok(GetDefaultResponse(await _service.CreateOrUpdateRangeAsync(lista)));
        }
        [HttpPatch("{id}/{estado}")]
        public async Task<IActionResult> UpdateStateAsync(int id, bool estado)
        {
            //Se obtiene el AzureID del token
            var Dto = new NovedadesPatchDTO()
            {
                ResponsableID = Guid.NewGuid(),
                id = id,
                estado = estado
            };
            return Ok(GetDefaultResponse(await _service.UpdateStateAsync(Dto)));
        }
        public ResponseDTO<T> GetDefaultResponse<T>(T dato)
        {
            return new ResponseDTO<T>
            {
                Title = "Succesfull",
                Status = 200,
                Data = dato
            };
        }
    }
}
