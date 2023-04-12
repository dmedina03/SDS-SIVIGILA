using FluentValidation;
using FluentValidation.Results;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.NovedadesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.NovedadesService.Utils;

namespace SIVIGILA.Service.NovedadesService
{
    public class NovedadService: INovedadService
    {
        private readonly INovedadRepository _repository;
        private readonly IValidator<NovedadesDTO> _validator;
        public NovedadService(INovedadRepository repository, IValidator<NovedadesDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> AddAsync(NovedadesDTO Dto)
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

        public async Task<bool> CreateOrUpdateRangeAsync(IEnumerable<NovedadesDTO> Dtos)
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
                        dataValidationError.Add($"LineaDTO[{i}].{element.Key}", element.Value);

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

        public async Task<bool> ExistByNameAsync(string nombreNovedad)
        {
            return await _repository.
                ExistGenericAsync(x => x.NombreNovedad.ToLower().Trim() == nombreNovedad.ToLower().Trim());
        }

        public async Task<IEnumerable<NovedadesDTO>> GetAllAsync()
        {
            var data = await _repository.GenericGetAllAsync(x => x.Estado, null, null, x => x);
            return data.MapToDTO();
        }

        public async Task<IEnumerable<string>> GetAllNamesAsync(string? search)
        {
            if (!String.IsNullOrEmpty(search))
                return await _repository.GenericGetAllValuesAsync(x => x.NombreNovedad, x => x.NombreNovedad.StartsWith(search));

            return await _repository.GenericGetAllValuesAsync(x => x.NombreNovedad);
        }

        public async Task<NovedadesDTO> GetByIdAsync(int Id)
        {
            var data = await _repository.GenericGetAsync(x => new NovedadesDTO
            {
                NovedadesID = x.Id,
                NombreNovedad = x.NombreNovedad,
                Estado = x.Estado
            }, x => x.Id == Id);
            if (data is null)
                throw new NotFoundException($"No se encontró una Novedad con el ID {Id}");
            return data;
        }

        public async Task<DataCollection<NovedadesDTO>> GetByParamsAsync(SearchNovedadesDTO Dto)
        {
            return await _repository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(NovedadesDTO Dto)
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

        public async Task<bool> UpdateStateAsync(NovedadesPatchDTO Dto)
        {
            var data = await _repository.GenericGetAsync(x => new Novedades
            {
                Id = x.Id,
                Estado = x.Estado,
                Responsable=Dto.ResponsableID
            }, x => x.Id == Dto.id);
            if (data is null)
                throw new NotFoundException($"No se encontró una Novedad con el ID {Dto.id}");
            data.Estado = Dto.estado;
            data.Responsable = Dto.ResponsableID;
            return await _repository.UpdateGenericAsync(data, true, x => x.Estado,x=>x.Responsable);
        }

        private void VerifyDuplicades(Dictionary<string, string[]> errors, IEnumerable<NovedadesDTO> lista)
        {
            //Verificamos duplicados por ID
            var dataIds = lista.GroupBy(x => x.NovedadesID).Select(x => new { ID = x.Key, Count = x.Count() })
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
            var dataNames = lista.GroupBy(x => x.NombreNovedad.Trim()).Select(x => new { Name = x.Key, Count = x.Count() })
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
