using FluentValidation;
using SIVIGILA.Commons.DTOs.Profesion;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Commons.Utils.ExtenssionMethods;
using SIVIGILA.Service.ProfesionService.Utils;
using FluentValidation.Results;
using SIVIGILA.Models.Entities;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Commons.DTOs.Search;

namespace SIVIGILA.Service.ProfesionService
{
    public class ProfesionService : IProfesionService
    {
        private readonly IProfesionRepository _repository;
        private readonly IValidator<ProfesionDTO> _validator;

        public ProfesionService(IProfesionRepository repository, IValidator<ProfesionDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> AddAsync(ProfesionDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opt => opt.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entity = Dto.MapToEntity();
            bool IsComplete = await _repository.AddAsync(entity);
            return entity.Id;

        }

        public async Task<bool> CreateOrUpdateRangeAsync(IEnumerable<ProfesionDTO> Dtos)
        {
            Dictionary<string, string[]> dataValidationError = new();
            ValidationResult result;
            int i = 0;

            //Verificamos duplicados
            VerifyDuplicades(dataValidationError, Dtos);

            foreach (var dto in Dtos)
            {
                result = await _validator.ValidateAsync(dto, opt => opt.IncludeRuleSets("Any"));
                if (!result.IsValid)
                {
                    foreach (var element in result.ToDictionary())
                    {
                        dataValidationError.Add($"ProfesionDTO[{i}].{element.Key}", element.Value);

                    }
                }
                i++;
            }
            if (dataValidationError.Count() > 0)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                dataValidationError);
            }
            var entities = Dtos.MapToEntity();

            List<Task<bool>> tasks = new();

            //Se seleccionan las que se van a actualizar
            var toUpdate = entities.Where(x => x.Id != 0);
            if (toUpdate.Count() != 0)
                tasks.Add(_repository.UpdateRangeAsync(toUpdate, false));
            var toCreate = entities.Where(x => x.Id == 0);
            if (toCreate.Count() != 0)
                tasks.Add(_repository.AddRangeAsync(toCreate, false));
            var dataComplete = await Task.WhenAll(tasks);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistByIDAsync(int ID)
        {
            return await _repository.ExistGenericAsync(x => x.Id == ID);
        }

        public async Task<bool> ExistByNameAsync(string nombreProfesion)
        {
            return await _repository.
                ExistGenericAsync(x => x.NombreProfesion.ToLower().Trim() == nombreProfesion.ToLower().Trim());
        }

        public async Task<IEnumerable<ProfesionDTO>> GetAllAsync()
        {
            var data = await _repository.GenericGetAllAsync(x => x.Estado, null, null, x => x);
            return data.MapToDTO();
        }

        public async Task<ProfesionDTO> GetByIdAsync(int Id)
        {
            var data = await _repository.GenericGetAsync(x => new ProfesionDTO
            {
                ProfesionID = x.Id,
                NombreProfesion = x.NombreProfesion,
                Estado = x.Estado
            }, x => x.Id == Id);
            if (data is null)
                throw new NotFoundException($"No se encontró una Profesion con el ID {Id}");
            return data;
        }

        public async Task<DataCollection<ProfesionDTO>> GetByParamsAsync(SearchProfesionDTO Dto)
        {
            return await _repository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(ProfesionDTO Dto)
        {
            var result = await _validator.ValidateAsync(Dto, opts => opts.IncludeRuleSets("Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entity = Dto.MapToEntity();
            return await _repository.UpdateGenericAsync(entity);
        }

        public async Task<bool> UpdateStateAsync(int ID, bool estado)
        {
            var data = await _repository.GenericGetAsync(x => new Profesion
            {
                Id = x.Id,
                Estado = x.Estado
            }, x => x.Id == ID);
            if (data is null)
                throw new NotFoundException($"No se encontró una Profesion con el ID {ID}");
            data.Estado = estado;
            return await _repository.UpdateGenericAsync(data, true, x => x.Estado);
        }

        private void VerifyDuplicades(Dictionary<string, string[]> errors, IEnumerable<ProfesionDTO> lista)
        {
            //Verificamos duplicados por ID
            var dataIds = lista.GroupBy(x => x.ProfesionID).Select(x => new { ID = x.Key, Count = x.Count() })
                        .Where(x => x.Count > 1 && x.ID != 0);
            var errores = new List<string>();
            if (dataIds.Count() > 0)
            {
                foreach (var item in dataIds)
                {
                    errores.Add($"El Dto con el Id {item.ID} se repite {item.Count} veces en el JSON");
                }
            }
            // Verificamos Duplicados por nombre
            var dataNames = lista.GroupBy(x => x.NombreProfesion.Trim()).Select(x => new { Name = x.Key, Count = x.Count() })
                        .Where(x => x.Count > 1);
            if (dataNames.Count() > 0)
            {
                foreach (var item in dataNames)
                {
                    errores.Add($"El nombre {item.Name} se repite {item.Count} veces en el JSON");
                }
            }

            if (errores.Count() > 0)
                errors.Add("Valores repetidos", errores.ToArray());

        }
    }
}
