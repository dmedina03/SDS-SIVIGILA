using FluentValidation;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.DpPresenEtnicaServices.Utils;
using System.Runtime.InteropServices;

namespace SIVIGILA.Service.DpPresenEtnicaServices
{
    public class DpPresenEtnicaService : IDpPresenEtnicaService
    {
        private readonly IDpPresenEtnicaRepository _dpPresenEtnicaRepository;
        private readonly IValidator<DpPresenEtnicaDTO> _validator;
        public DpPresenEtnicaService(IDpPresenEtnicaRepository dpPresenEtnicaRepository,
                                    IValidator<DpPresenEtnicaDTO> validator)
        {
            _dpPresenEtnicaRepository = dpPresenEtnicaRepository;
            _validator = validator;
        }
        public async Task<int> AddAsync(DpPresenEtnicaDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var data = Dto.MapToEntity();
            await _dpPresenEtnicaRepository.AddAsync(data);
            return data.Id;
        }

        public async Task<IEnumerable<DpPresenEtnicaGetDTO>> GetAllAsync()
        {
            var entities = await _dpPresenEtnicaRepository.GenericGetAllAsync();
            return entities.MapToDTO();
        }

        public async Task<DpPresenEtnicaGetDTO> GetByIdAsync(int Id)
        {
            var data = await _dpPresenEtnicaRepository.GenericGetAsync(x => new DpPresenEtnicaGetDTO
            {
                ID = x.Id,
                Descripcion = x.Descripcion,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
                Vsa = x.Vsa
            }, x => x.Id == Id);
            return data;
        }

        public async Task<DataCollection<DpPresenEtnicaDTO>> GetByParamsAsync(SearchDpPrensenEtnicaDTO Dto)
        {
            return await _dpPresenEtnicaRepository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(DpPresenEtnicaDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Update", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var data = Dto.MapToEntity();
            return await _dpPresenEtnicaRepository.UpdateGenericAsync(data);
        }
    }
}
