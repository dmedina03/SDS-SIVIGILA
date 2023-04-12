using FluentValidation;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.DpSexoServices.Utils;

namespace SIVIGILA.Service.DpSexoService
{
    public class DpSexoService : IDpSexoService
    {
        private readonly IDpSexoRepository _dpSexoRepository;
        private readonly IValidator<DpSexoDTO> _validator;
        public DpSexoService(IDpSexoRepository dpSexoRepository, IValidator<DpSexoDTO> validator)
        {
            _dpSexoRepository = dpSexoRepository;
            _validator = validator;
        }

        public async Task<int> AddAsync(DpSexoDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = Dto.MapToEntity();
            var save = await _dpSexoRepository.AddAsync(entidad);
            return entidad.Id;
        }

        public async Task<IEnumerable<DpSexoDTO>> GetAllAsync()
        {
            var data = await _dpSexoRepository.GenericGetAllAsync();
            return data.MapToDto();
        }

        public async Task<DpSexoGetDTO> GetByIdAsync(int Id)
        {
            var data = await _dpSexoRepository.GenericGetAsync(x => new DpSexoGetDTO()
            {
                ID = x.Id,
                Descripcion = x.Descripcion,
                Ivc = x.Ivc,
                TalentoHumano = x.TalentoHumano,
                Vsa = x.Vsa
            }, x => x.Id == Id);
            if (data is null)
            {
                throw new NotFoundException($"No se encontro un Dato Poblacional SEXO con ID {Id}");
            }
            return data;
        }

        public async Task<DataCollection<DpSexoDTO>> GetByParamsAsync(SearchDpSexoDTO Dto)
        {
            return await _dpSexoRepository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(DpSexoDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Update", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = Dto.MapToEntity();
            return await _dpSexoRepository.UpdateGenericAsync(entidad);
        }
    }
}
