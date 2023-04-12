using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Models.Entities;
using SIVIGILA.Service.TipoUbicacionService;

namespace SIVIGILA.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class TipoUbicacionController : ControllerBase
    {
        private readonly ITipoUbicacionService _tipoUbicacionService;
        public TipoUbicacionController(ITipoUbicacionService tipoUbicacionService)
        {
            _tipoUbicacionService = tipoUbicacionService;
        }

        [HttpPost]
        public async Task<ActionResult> SaveTipoUbicacion(TipoUbicacionDto tipoUbicacion)
        {
            return Ok(GetDefaultResponse(await _tipoUbicacionService.AddAsync(tipoUbicacion)));
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateTipoUbicacion(TipoUbicacionDto tipoUbicacion)
        {
            return Ok(GetDefaultResponse(await _tipoUbicacionService.UpdateAsync(tipoUbicacion)));
        }
        [HttpGet("Active/All")]
        public async Task<ActionResult> GetAllTipoUbicacion()
        {
            return Ok(GetDefaultResponse(await _tipoUbicacionService.GetAllAsync()));
        }
        [HttpPost("Range")]
        public async Task<ActionResult> SaveAndUpdateTipoUbicacion(IEnumerable<TipoUbicacionDto> listDto)
        {
            return Ok(GetDefaultResponse(await _tipoUbicacionService.CreateOrUpdateRangeAsync(listDto)));
        }
        [HttpGet("Search")]
        public async Task<ActionResult> GetByParams([FromQuery] SearchTipoUbicacionDTO dto)
        {
            return Ok(GetDefaultResponse(await _tipoUbicacionService.GetByParamsAsync(dto)));
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
