using FluentValidation;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.TipoDatoDTO;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.DetalleUbicacionService.Utils;

namespace SIVIGILA.Service.DetalleUbicacionService
{
    public class DetalleUbicacionService : IDetalleUbicacionService
    {
        private readonly IDetalleUbicacionRepository _detalleUbicacionRepository;
        private readonly IValidator<DetalleUbicacionDto> _validator;
        public DetalleUbicacionService(IDetalleUbicacionRepository detalleUbicacionRepository, IValidator<DetalleUbicacionDto> validator)
        {
            _detalleUbicacionRepository = detalleUbicacionRepository;
            _validator = validator;
        }

        public async Task<int> AddAsync(DetalleUbicacionDto detalleDto)
        {
            var result = await _validator.ValidateAsync(detalleDto, opt => opt.IncludeRuleSets("Any", "Create"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = detalleDto.MapToEntity();
            var data = await _detalleUbicacionRepository.AddAsync(entidad);
            return entidad.Id;
        }
        public async Task<bool> UpdateAsync(DetalleUbicacionDto detalleDto)
        {
            var result = await _validator.ValidateAsync(detalleDto, opt => opt.IncludeRuleSets("Update","Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = detalleDto.MapToEntity();
            return await _detalleUbicacionRepository.UpdateGenericAsync(entidad, true,
                            x => x.Detalle, x => x.Estado, x => x.FkTipoUbicacion); 
        }

        public async Task<IEnumerable<DetalleUbicacionGetDTO>> GetAllAsync()
        {
            var data = await _detalleUbicacionRepository.GenericGetAllAsync();
            return data.MapToDto();
        }

        public async Task<DetalleUbicacionGetDTO> GetByIdAsync(int Id)
        {
            var data = await _detalleUbicacionRepository.GenericGetAsync(x => new DetalleUbicacionGetDTO()
            {
                Id = x.Id,
                Detalle = x.Detalle,
                Estado = x.Estado,
                FkTipoUbicacion = x.FkTipoUbicacion
            }, x => x.Id == Id);
            return data;
        }
    }
}
