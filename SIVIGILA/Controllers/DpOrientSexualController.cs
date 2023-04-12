using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.DpOrientSexualService;

namespace SIVIGILA.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DpOrientSexualController : ControllerBase
    {
        private IDpOrientSexualService _DpOrientSexualService;

        public DpOrientSexualController(IDpOrientSexualService DpOrientSexualService)
        {
            _DpOrientSexualService = DpOrientSexualService;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> GetByParamsAsync([FromQuery] SearchDpOrientSexualDTO Dto)
        {
            return Ok(await _DpOrientSexualService.GetByParamsAsync(Dto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(GetDefaultResponse(await _DpOrientSexualService.GetByIdAsync(id)));
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(GetDefaultResponse(await _DpOrientSexualService.GetAllAsync()));
        }
        [HttpPost]
        public async Task<IActionResult> SaveDpOrientSexual(DpOrientSexualDTO Dto)
        {
            return Ok(GetDefaultResponse(await _DpOrientSexualService.AddAsync(Dto)));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDpOrientSexual(DpOrientSexualDTO Dto)
        {
            return Ok(GetDefaultResponse(await _DpOrientSexualService.UpdateAsync(Dto)));
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
