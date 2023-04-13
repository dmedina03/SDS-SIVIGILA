using FluentValidation;
using FluentValidation.Results;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.PerfilVigenciaService.Utils;

namespace SIVIGILA.Service.PerfilVigenciaService
{
    public class PerfilVigenciaService : IPerfilVigenciaService
    {

        private readonly IPerfilVigenciaRepository _perfilVigenciaRepository;
        private readonly IValidator<PerfilVigenciaDto> _validator;
        public PerfilVigenciaService(IPerfilVigenciaRepository perfilVigenciaRepository, IValidator<PerfilVigenciaDto> validator)
        {
            _perfilVigenciaRepository = perfilVigenciaRepository;
            _validator = validator; 
        }

        public async Task<int> AddAsync(PerfilVigenciaDto Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = Dto.MapToEntity();
            var data = await _perfilVigenciaRepository.AddAsync(entidad);
            return entidad.PerfilVigenciaID;
        }

        public async Task<bool> UpdateAsync(PerfilVigenciaDto Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Update","Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entidad = Dto.MapToEntity();
            var data = await _perfilVigenciaRepository.UpdateGenericAsync(entidad);
            return data;
        }

        public async Task<bool> CreateOrUpdateRangeAsync(IEnumerable<PerfilVigenciaDto> Dtos)
        {
            Dictionary<string, string[]> dataValidationError = new();
            ValidationResult result;
            int i = 0;
            VerifyDuplicades(dataValidationError, Dtos);
            foreach (var dto in Dtos)
            {
                result = await _validator.ValidateAsync(dto, opt => opt.IncludeRuleSets("Any"));
                if (!result.IsValid)
                {
                    foreach (var element in result.ToDictionary())
                    {
                        dataValidationError.Add($"PerfilVigenciaDTO[{i}].{element.Key}", element.Value);
                    }
                }
                i++;
            }
            if (dataValidationError.Count() > 0)
            {
                throw new ValidationDataException("Se encontraron errores de validadcion",
                    dataValidationError);
            }

            var entities = Dtos.MapToEntity();
            List<Task<bool>> tasks = new();

            var toUpdate = entities.Where(x => x.PerfilVigenciaID != 0);
            if (toUpdate.Count() != 0)
                tasks.Add(_perfilVigenciaRepository.UpdateRangeAsync(toUpdate, false));
            var toCreate = entities.Where(x => x.PerfilVigenciaID == 0);
            if (toCreate.Count() != 0)
                tasks.Add(_perfilVigenciaRepository.AddRangeAsync(toCreate, false));
            await _perfilVigenciaRepository.SaveChangesAsync();
            return true;
        }

        private void VerifyDuplicades(Dictionary<string, string[]> errors, IEnumerable<PerfilVigenciaDto> lista)
        {
            //Verificamos duplicados por ID
            var dataIds = lista.GroupBy(x => x.PerfilVigenciaID).Select(x => new { ID = x.Key, Count = x.Count() })
                        .Where(x => x.Count > 1 && x.ID != 0);
            var errores = new List<string>();
            if (dataIds.Count() > 0)
            {
                foreach (var item in dataIds)
                {
                    errores.Add($"El Dto con el Id {item.ID} se repite {item.Count} veces en el JSON");
                }
            }

            if (errores.Count() > 0)
                errors.Add("Valores repetidos", errores.ToArray());
        }

        public async Task<DataCollection<PerfilVigenciaGetDTO>> GetByParamsAsync(SearchPerfilVigenciaDTO Dto)
        {
            return await _perfilVigenciaRepository.GetByParamsAsync(Dto);
        }
        public async Task<IEnumerable<PerfilVigenciaTablaCostoDTO>> GetPerfilesByIdVigencia(int Id)
        {
            return await _perfilVigenciaRepository.PerfilesByIdVigencia(Id);
        }

    }
}
