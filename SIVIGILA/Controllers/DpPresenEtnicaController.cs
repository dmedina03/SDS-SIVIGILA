using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.DpPresenEtnicaServices;

namespace SIVIGILA.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DpPresenEtnicaController : ControllerBase
    {

        private readonly IDpPresenEtnicaService _service;
        public DpPresenEtnicaController(IDpPresenEtnicaService service)
        {
            _service = service;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetByIDAsync(int Id)
        {
            return Ok(GetDefaultResponse(await _service.GetByIdAsync(Id)));
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(GetDefaultResponse(await _service.GetAllAsync()));
        }
        [HttpPost]
        public async Task<ActionResult> SaveAsync(DpPresenEtnicaDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.AddAsync(Dto)));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(DpPresenEtnicaDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.UpdateAsync(Dto)));
        }
        [HttpGet("Range")]
        public async Task<ActionResult> GetByParamsAsync([FromQuery] SearchDpPrensenEtnicaDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.GetByParamsAsync(Dto)));
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
