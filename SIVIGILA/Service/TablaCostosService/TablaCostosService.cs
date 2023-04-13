using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Service.TablaCostosService.Utils;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using FluentValidation;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;

namespace SIVIGILA.Service.TablaCostosService
{
    public class TablaCostosService : ITablaCostosService
    {
        private readonly ITablaCostosRepository _tablaCostosRepository;
        private readonly IValidator<TablaCostosDTO> _validator;
        public TablaCostosService(ITablaCostosRepository tablaCostosRepository, IValidator<TablaCostosDTO> validator)
        {
            _tablaCostosRepository = tablaCostosRepository;
            _validator = validator;
        }
        public async Task<int> AddAsync(TablaCostosDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Create","Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontraron errores de validacion",
                    result.ToDictionary());
            }
            var entitidad = Dto.MapToEntity();
            await _tablaCostosRepository.AddAsync(entitidad);
            return entitidad.ID;
        }

        public async Task<bool> AddRangeAsync(List<TablaCostosDTO> Dtos)
        {
            var entitidad = Dtos.MapToEntity();
            return await _tablaCostosRepository.AddRangeAsync(entitidad);
        }
    }
}
