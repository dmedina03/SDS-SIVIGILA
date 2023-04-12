using FluentValidation;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.DpCondiDiscapaService.Utils;

namespace SIVIGILA.Service.DpCondiDiscapaService
{
    public class DpCondiDiscapaService : IDpCondiDiscapaService
    {
        private readonly IDpCondiDiscapaRepository _dpCondiDiscapaRepository;
        private readonly IValidator<DpCondiDiscapaDTO> _validator;
        public DpCondiDiscapaService(IDpCondiDiscapaRepository dpCondiDiscapaRepository, IValidator<DpCondiDiscapaDTO> validator)
        {
            _dpCondiDiscapaRepository = dpCondiDiscapaRepository;
            _validator = validator;
        }
        public async Task<int> AddAsync(DpCondiDiscapaDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var data = Dto.MapToEntity();
            await _dpCondiDiscapaRepository.AddAsync(data);
            return data.Id;
        }

        public async Task<IEnumerable<DpCondiDiscapaDTO>> GetAllAsync()
        {
            var entities = await _dpCondiDiscapaRepository.GenericGetAllAsync();
            return entities.MapToDTO();
        }

        public async Task<DpCondiDiscapaDTO> GetByIdAsync(int Id)
        {
            var data = await _dpCondiDiscapaRepository.GenericGetAsync(x => new DpCondiDiscapaDTO
            {
                ID = x.Id,
                Descripcion = x.Descripcion,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
                Vsa = x.Vsa
            }, x => x.Id == Id);
            return data;
        }

        public async Task<DataCollection<DpCondiDiscapaDTO>> GetByParamsAsync(SearchDpCondiDiscapaDTO Dto)
        {
            return await _dpCondiDiscapaRepository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(DpCondiDiscapaDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Update", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var data = Dto.MapToEntity();
            return await _dpCondiDiscapaRepository.UpdateGenericAsync(data);
        }
    }
}
