using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SIVIGILA.Commons.DTOs.Actividad;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.MetaService.Utils;

namespace SIVIGILA.Service.MetaService
{
    public class MetaService: IMetaService
    {
        private readonly IMetaRepository _repository;
        private readonly IValidator<MetaDTO> _validator;
        public MetaService(IMetaRepository repository, IValidator<MetaDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> AddAsync(MetaDTO Dto)
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

        public async Task<IEnumerable<MetaGetDTO>> CopyInfoInLastVigenciaAsync()
        {
            var LastIDVigencia = await _repository.ApplyOperationAsync(x => x.MaxAsync(c => c.FkVigencia));
            if (LastIDVigencia == 0)    //Si no hay Datos muestra una lista vacia
                return new List<MetaGetDTO>();
            //Si existen datos trae los datos de Meta y Actividad de la última Vigencia cargada
            var data = await _repository.
                    GenericGetAllAsync(x => x.FkVigencia == LastIDVigencia,x=>x.Include(c=>c.Actividads));
            return data.MapToDto(false);
        }

        public async Task<bool> CreateOrUpdateRangeAsync(IEnumerable<MetaDTO> Dtos)
        {
            Dictionary<string, string[]> dataValidationError = new();
            ValidationResult result;
            int i = 0;

            VerifyDuplicades(dataValidationError,Dtos);

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

        public async Task<IEnumerable<MetaGetDTO>> GetAllAsync()
        {
            var data = await _repository.GenericGetAllAsync();
            return data.MapToDto();
        }

        public async Task<MetaGetDTO> GetByIdAsync(int Id)
        {
            var data = await _repository.GenericGetAsync(x => new MetaGetDTO
            {
                MetaId = x.Id,
                NombreMeta=x.NombreMeta,
                DetalleMeta=x.DetalleMeta,
                VigenciaID=x.FkVigencia,
                Actividades = x.Actividads.Select(c=> new ActividadGetDTO
                {
                    ActividadID = c.Id,
                    NombreActividad = c.NombreActividad,
                    DetalleActividad = c.DetalleActividad,
                    MetaActividad = x.NombreMeta + " - " + c.NombreActividad,
                    Estado = c.Estado,
                    MetaID = c.FkMeta
                })
            }, x => x.Id == Id);
            if (data is null)
                throw new NotFoundException($"No se encontró una Meta con el ID {Id}");
            return data;
        }

        public Task<DataCollection<MetaGetDTO>> GetByParamsAsync(SearchMetaDTO Dto)
        {
            return _repository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(MetaDTO Dto)
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

        private void VerifyDuplicades(Dictionary<string, string[]> errors, IEnumerable<MetaDTO> lista)
        {
            //Verificamos duplicados por ID
            var dataIds = lista.GroupBy(x => x.MetaId).Select(x => new { ID = x.Key, Count = x.Count() })
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
    }
}
