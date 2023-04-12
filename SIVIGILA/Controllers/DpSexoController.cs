using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.DpSexoService;

namespace SIVIGILA.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DpSexoController : ControllerBase
    {
        private IDpSexoService _dpSexoService;

        public DpSexoController(IDpSexoService dpSexoService)
        {
            _dpSexoService = dpSexoService;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> GetByParamsAsync([FromQuery] SearchDpSexoDTO Dto)
        {
            return Ok(await _dpSexoService.GetByParamsAsync(Dto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(GetDefaultResponse(await _dpSexoService.GetByIdAsync(id)));
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            return Ok(GetDefaultResponse(await _dpSexoService.GetAllAsync()));
        }
        [HttpPost]
        public async Task<IActionResult> SaveDpSexo(DpSexoDTO Dto)
        {
            return Ok(GetDefaultResponse(await _dpSexoService.AddAsync(Dto)));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDpSexo(DpSexoDTO Dto)
        {
            return Ok(GetDefaultResponse(await _dpSexoService.UpdateAsync(Dto)));
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
