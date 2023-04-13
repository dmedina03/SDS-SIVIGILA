using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.PerfilVigenciaService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PerfilVigenciaController : ControllerBase
    {
        private readonly IPerfilVigenciaService _perfilVigenciaService;
        public PerfilVigenciaController(IPerfilVigenciaService perfilVigenciaService)
        {
            _perfilVigenciaService = perfilVigenciaService;
        }

        [HttpPost]
        public async Task<ActionResult> SavePerfilVigencia(PerfilVigenciaDto Dto)
        {
            return Ok(GetDefaultResponse(await _perfilVigenciaService.AddAsync(Dto)));
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePerfilVigencia(PerfilVigenciaDto Dto)
        {
            return Ok(GetDefaultResponse(await _perfilVigenciaService.UpdateAsync(Dto)));
        }

        [HttpPost("Range")]
        public async Task<ActionResult> CreateOrUpdateRangePerfilVigencia(IEnumerable<PerfilVigenciaDto> Dtos)
        {
            return Ok(GetDefaultResponse(await _perfilVigenciaService.CreateOrUpdateRangeAsync(Dtos)));
        }
        [HttpGet("Search")]
        public async Task<ActionResult> CreateOrUpdateRangePerfilVigencia([FromQuery] SearchPerfilVigenciaDTO Dto)
        {
            return Ok(GetDefaultResponse(await _perfilVigenciaService.GetByParamsAsync(Dto)));
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
