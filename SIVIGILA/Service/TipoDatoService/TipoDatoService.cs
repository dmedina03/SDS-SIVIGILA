using FluentValidation;
using SIVIGILA.Commons.DTOs.TipoDatoDto;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.TipoDatoDTO;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.TipoDatoService.Utils;

namespace SIVIGILA.Service.TipoDatoService
{
    public class TipoDatoService : ITipoDatoService
    {
        private readonly ITipoDatoRepository _tipoDatoRepository;
        private readonly IValidator<TipoDatoDTO> _validator;
        public TipoDatoService(ITipoDatoRepository tipoDatoRepository, IValidator<TipoDatoDTO> validator)
        {
            _tipoDatoRepository = tipoDatoRepository;
            _validator = validator;
        }

        public async Task<int> AddAsync(TipoDatoDTO tipoDatoDto)
        {
            var result = await _validator.ValidateAsync(tipoDatoDto, opt => opt.IncludeRuleSets("DescripcionEqual"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = tipoDatoDto.MapToEntity();
            var data = await _tipoDatoRepository.AddAsync(entidad);
            return entidad.Id;
        }

        public async Task<bool> UpdateAsync(TipoDatoDTO tipoDatoDto)
        {
            var result = await _validator.ValidateAsync(tipoDatoDto, opt => opt.IncludeRuleSets("Update", "DescripcionEqual"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = tipoDatoDto.MapToEntity();
            return await _tipoDatoRepository.UpdateGenericAsync(entidad, true, X => X.Descripcion);
        }

        public async Task<IEnumerable<TipoDatoGetDTO>> GetAllAsync()
        {
            var data = await _tipoDatoRepository.GenericGetAllAsync();
            return data.MapToDto();
        }

        public async Task<TipoDatoGetDTO> GetByIdAsync(int Id)
        {
            var data = await _tipoDatoRepository.GenericGetAsync(x => new TipoDatoGetDTO()
            {
                Id = x.Id,
                Descripcion = x.Descripcion

            }, x => x.Id == Id);
            return data;
        }
    }
}
