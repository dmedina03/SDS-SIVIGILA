using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Service.DetalleUbicacionService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DetalleUbicacionController : ControllerBase
    {
        private readonly IDetalleUbicacionService _detallleUbicacionService;
        public DetalleUbicacionController(IDetalleUbicacionService detallleUbicacionService)
        {
            _detallleUbicacionService = detallleUbicacionService;
        }

        [HttpPost]
        public async Task<ActionResult> SaveDetalleUbicacion(DetalleUbicacionDto dto)
        {
            var data = await _detallleUbicacionService.AddAsync(dto);
            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDetalleUbicacion(DetalleUbicacionDto dto)
        {
            var data = await _detallleUbicacionService.UpdateAsync(dto);
            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDetalleUbicacion()
        {
            var data = await _detallleUbicacionService.GetAllAsync();
            var response = GetDefaultResponse(data);
            return Ok(response);
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
