using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.TerminalesPortuario;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.TerminalesPortuarioService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TerminalesPortuarioController : ControllerBase
    {
        private readonly ITerminalesPortuarioService _service;
        public TerminalesPortuarioController(ITerminalesPortuarioService service)
        {
            _service = service;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> GetByParamsAsync([FromQuery] SearchTerminalesPortuarioDTO Dto)
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
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TerminalesPortuarioDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.AddAsync(Dto)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, TerminalesPortuarioDTO Dto)
        {
            Dto.TerminalesPortuarioID = id;
            return Ok(GetDefaultResponse(await _service.UpdateAsync(Dto)));
        }
        [HttpPost("Range")]
        public async Task<IActionResult> CreateOrUpdateRangeAsync(IEnumerable<TerminalesPortuarioDTO> lista)
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
