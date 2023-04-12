using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.ProductosVigenciaServices;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductosVigenciaController : ControllerBase
    {
        private readonly IProductoVigenciaService _service;
        public ProductosVigenciaController(IProductoVigenciaService service)
        {
            _service = service;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetByParamsAsync([FromQuery] SearchProductoVigenciaDTO Dto)
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
        [HttpGet("Name/All")]
        public async Task<IActionResult> GetAllnamesAsync([FromQuery] string? search)
        {
            return Ok(GetDefaultResponse(await _service.GetAllNamesAsync(search)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductosVigenciaDTO Dto)
        {
            //Se obtiene el AzureID del token
            Dto.ResponsableID = Guid.NewGuid();
            return Ok(GetDefaultResponse(await _service.AddAsync(Dto)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, ProductosVigenciaDTO Dto)
        {
            //Se obtiene el AzureID del token
            Dto.ResponsableID = Guid.NewGuid();
            Dto.ProductoVigenciaID = id;
            return Ok(GetDefaultResponse(await _service.UpdateAsync(Dto)));
        }
        [HttpPost("Range")]
        public async Task<IActionResult> CreateOrUpdateRangeAsync(IEnumerable<ProductosVigenciaDTO> lista)
        {
            //Se obtiene el AzureID del token
            Guid ResponsableID = Guid.NewGuid(); ;
            foreach (var item in lista)
            {
                item.ResponsableID=ResponsableID;
            }
            return Ok(GetDefaultResponse(await _service.CreateOrUpdateRangeAsync(lista)));
        }
        [HttpPatch("{id}/{estado}")]
        public async Task<IActionResult> UpdateStateAsync(int id, bool estado)
        {
            //Se obtiene el AzureID del token
            var Dto = new ProductoVigenciaPatchDTO()
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
                Title = "Succesfull",
                Status = 200,
                Data = dato
            };
        }

    }
}
