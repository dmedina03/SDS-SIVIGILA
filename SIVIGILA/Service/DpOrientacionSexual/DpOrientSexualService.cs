using FluentValidation;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.DpOrientSexualServices.Utils;

namespace SIVIGILA.Service.DpOrientSexualService
{
    public class DpOrientSexualService : IDpOrientSexualService
    {
        private readonly IDpOrientSexualRepository _DpOrientSexualRepository;
        private readonly IValidator<DpOrientSexualDTO> _validator;
        public DpOrientSexualService(IDpOrientSexualRepository DpOrientSexualRepository, IValidator<DpOrientSexualDTO> validator)
        {
            _DpOrientSexualRepository = DpOrientSexualRepository;
            _validator = validator;
        }

        public async Task<int> AddAsync(DpOrientSexualDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = Dto.MapToEntity();
            var save = await _DpOrientSexualRepository.AddAsync(entidad);
            return entidad.Id;
        }

        public async Task<IEnumerable<DpOrientSexualDTO>> GetAllAsync()
        {
            var data = await _DpOrientSexualRepository.GenericGetAllAsync();
            return data.MapToDto();
        }

        public async Task<DpOrientSexualGetDTO> GetByIdAsync(int Id)
        {
            var data = await _DpOrientSexualRepository.GenericGetAsync(x => new DpOrientSexualGetDTO()
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

        public async Task<DataCollection<DpOrientSexualDTO>> GetByParamsAsync(SearchDpOrientSexualDTO Dto)
        {
            return await _DpOrientSexualRepository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(DpOrientSexualDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Update", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = Dto.MapToEntity();
            return await _DpOrientSexualRepository.UpdateGenericAsync(entidad);
        }
    }
}
