using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.MetaService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private readonly IMetaService _service;

        public MetaController(IMetaService metaService)
        {
            _service = metaService;
        }
        [HttpGet("/api/v1/Vigencia/{VigenciaID}/Meta/Search")]
        public async Task<IActionResult> GetByParamsAsync([FromQuery] SearchMetaDTO Dto)
        {
            return Ok(await _service.GetByParamsAsync(Dto));
        }
        [HttpGet("/api/v1/Vigencia/Meta/CopyLastInfo")]
        public async Task<IActionResult> CopyLastInfoAsync()
        {
            return Ok(await _service.CopyInfoInLastVigenciaAsync());
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
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MetaDTO Dto)
        {
            //Se obtiene el AzureID del token
            Dto.ResponsableID = Guid.NewGuid();
            foreach (var item in Dto.Actividades)
            {
                item.ResponsableID = Dto.ResponsableID;
            }
            return Ok(GetDefaultResponse(await _service.AddAsync(Dto)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, MetaDTO Dto)
        {
            //Se obtiene el AzureID del token
            Dto.ResponsableID = Guid.NewGuid();
            foreach (var item in Dto.Actividades)
            {
                item.ResponsableID = Dto.ResponsableID;
            }
            Dto.MetaId = id;
            return Ok(GetDefaultResponse(await _service.UpdateAsync(Dto)));
        }
        [HttpPost("Range")]
        public async Task<IActionResult> CreateOrUpdateRangeAsync(IEnumerable<MetaDTO> lista)
        {
            var ResponsableID = Guid.NewGuid(); //Se obtiene del Token
            foreach (var dto in lista)
            {
                dto.ResponsableID = ResponsableID;
                foreach (var item in dto.Actividades)
                {
                    item.ResponsableID = ResponsableID;
                }
            }

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
