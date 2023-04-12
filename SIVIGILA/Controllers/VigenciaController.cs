using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.Vigencia;
using SIVIGILA.Service.VigenciaService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VigenciaController : ControllerBase
    {
        private readonly IVigenciaService _service;
        public VigenciaController(IVigenciaService Service)
        {
            _service = Service;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetByParamsAsync([FromQuery]SearchVigenciaDTO Dto)
        {
            return Ok(await _service.GetByParamsAsync(Dto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(GetDefaultResponse(await _service.GetByIdAsync(id)));
        }
        [HttpGet("Active/All")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(GetDefaultResponse(await _service.GetAllAsync()));
        }
        [HttpGet("Active/Short")]
        public async Task<IActionResult> GetShortInfoAsync()
        {
            return Ok(GetDefaultResponse(await _service.GetShortInfoActiveDTO()));
        }
        [HttpGet("States")]
        public async Task<IActionResult> GetStatesAsync()
        {
            return Ok(GetDefaultResponse(await _service.GetStatesVigenciaAsync()));
        }
        [HttpPost("save")]
        public async Task<ActionResult> SaveVigencias(VigenciaDTO vigenciaDTO)
        {
            var data = await _service.AddAsync(vigenciaDTO);
            var response = GetDefaultResponse(data);
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateVigencias(VigenciaDTO vigenciaDTO)
        {
            var data = await _service.UpdateAsync(vigenciaDTO);
            var response = GetDefaultResponse(data);
            return Ok(response);
        }
        [HttpPost("Range")]
        public async Task<IActionResult> CreateOrUpdateRangeAsync(IEnumerable<VigenciaDTO> lista)
        {
            return Ok(GetDefaultResponse(await _service.CreateOrUpdateRangeAsync(lista)));
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
