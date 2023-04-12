using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.DpCondiDiscapaService;
using SIVIGILA.Service.DpPresenEtnicaServices;

namespace SIVIGILA.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DpCondiDiscapaController : ControllerBase
    {
        private readonly IDpCondiDiscapaService _service;
        public DpCondiDiscapaController(IDpCondiDiscapaService service)
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
        [HttpGet("Range")]
        public async Task<ActionResult> GetByParamsAsync([FromQuery] SearchDpCondiDiscapaDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.GetByParamsAsync(Dto)));
        }
        [HttpPost]
        public async Task<ActionResult> SaveAsync(DpCondiDiscapaDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.AddAsync(Dto)));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(DpCondiDiscapaDTO Dto)
        {
            return Ok(GetDefaultResponse(await _service.UpdateAsync(Dto)));
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
