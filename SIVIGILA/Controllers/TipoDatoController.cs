using Microsoft.AspNetCore.Mvc;
using SIVIGILA.Commons.DTOs.Response;
using SIVIGILA.Commons.TipoDatoDTO;
using SIVIGILA.Service.TipoDatoService;

namespace SIVIGILA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TipoDatoController : ControllerBase
    {
        private readonly ITipoDatoService _tipoDatoService;
        public TipoDatoController(ITipoDatoService tipoDatoService)
        {
            _tipoDatoService = tipoDatoService;
        }

        [HttpPost]
        public async Task<ActionResult> SaveTipoDato(TipoDatoDTO dto)
        {
            var data = await _tipoDatoService.AddAsync(dto);
            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTipoDato()
        {
            var data = await _tipoDatoService.GetAllAsync();
            var response = GetDefaultResponse(data);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTipoDato(TipoDatoDTO dto)
        {
            var data = await _tipoDatoService.UpdateAsync(dto);
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
