using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Service.PerfilVigenciaService;
using SIVIGILA.Service.TablaCostosService;

namespace SIVIGILA.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class TablaCostosController : ControllerBase
    {
        private readonly ITablaCostosService _service;
        private readonly IPerfilVigenciaService _perfilVigenciaService;
        public TablaCostosController(ITablaCostosService service, IPerfilVigenciaService perfilVigenciaService)
        {
            _service = service;
            _perfilVigenciaService = perfilVigenciaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(TablaCostosDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.AddAsync(Dto)));
        }
        
        [HttpPost("Range")]
        public async Task<IActionResult> AddRangeAsync(List<TablaCostosDTO> Dtos)
        {
            return Ok(GetDefaultResponse(await _service.AddRangeAsync(Dtos)));
        }
        [HttpGet("Perfiles/Vigencia/{Id}")]
        public async Task<ActionResult> GetPerfilesByVigenciaId(int Id)
        {
            return Ok(GetDefaultResponse(await _perfilVigenciaService.GetPerfilesByIdVigencia(Id)));
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
