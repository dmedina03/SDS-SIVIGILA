using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Actividad;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.ActividadService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly IActividadService _service;
        public ActividadController(IActividadService service)
        {
            _service = service;  
        }
        [HttpGet("/api/v1/Vigencia/{VigenciaID}/Meta/Actividad/Active/All")]
        public async Task<IActionResult> GetByParamsAsync([FromRoute] int VigenciaID)
        {
            return Ok(await _service.GetAllByVigenciaIDAsync(VigenciaID));
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
        [HttpPatch("{id}/{estado}")]
        public async Task<IActionResult> UpdateStateAsync(int id, bool estado)
        {
            //Se obtiene el AzureID del token
            var Dto = new ActividadPatchDTO()
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
                Title= "Succesfull",
                Status = 200,
                Data = dato
            };
        }
    }
}
