using FluentValidation;
using FluentValidation.Results;
using SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.DocumentoContServices.Utils;

namespace SIVIGILA.Service.DocumentoContServices
{
    public class DocumentoContService: IDocumentoContService
    {
        private readonly IDocumentosContRepository _repository;
        private readonly IValidator<DocumentoCDTO> _validator;
        public DocumentoContService(IDocumentosContRepository repositrory, IValidator<DocumentoCDTO> validator)
        {
            _repository = repositrory;
            _validator = validator;
        }

        public async Task<int> AddAsync(DocumentoCDTO Dto)
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

        public async Task<bool> CreateOrUpdateRangeAsync(IEnumerable<DocumentoCDTO> Dtos)
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

        public async Task<bool> ExistByNameAsync(string nombreDocumento)
        {
            return await _repository.
                ExistGenericAsync(x => x.NombreDocumento.ToLower().Trim() == nombreDocumento.ToLower().Trim());
        }

        public async Task<IEnumerable<DocumentoCDTO>> GetAllAsync()
        {
            var data = await _repository.GenericGetAllAsync(x => x.Estado);
            return data.MapToDTO();
        }

        public async Task<IEnumerable<string>> GetAllNamesAsync(string? search)
        {
            if (!String.IsNullOrEmpty(search))
                return await _repository.GenericGetAllValuesAsync(x => x.NombreDocumento, x => x.NombreDocumento.StartsWith(search));

            return await _repository.GenericGetAllValuesAsync(x => x.NombreDocumento);
        }

        public async Task<DocumentoCDTO> GetByIdAsync(int Id)
        {
            var data = await _repository.GenericGetAsync(x => new DocumentoCDTO
            {
                DocumentoConID = x.Id,
                NombreDocumento = x.NombreDocumento,
                Estado = x.Estado
            }, x => x.Id == Id);
            if (data is null)
                throw new NotFoundException($"No se encontró un documento con el ID {Id}");
            return data;
        }

        public async Task<DataCollection<DocumentoCDTO>> GetByParamsAsync(SearchDocumentoDTO Dto)
        {
            return await _repository.GetByParamsAsync(Dto);
        }

        public async Task<bool> UpdateAsync(DocumentoCDTO Dto)
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

        public async Task<bool> UpdateStateAsync(DocumentacionCPatchDTO Dto)
        {
            var data = await _repository.GenericGetAsync(x => new DocumentacionContratacion
            {
                Id = x.Id,
                Estado = x.Estado,
                Responsable=x.Responsable
            }, x => x.Id == Dto.id);
            if (data is null)
                throw new NotFoundException($"No se encontró un documento con el ID {Dto.id}");
            data.Estado = Dto.estado;
            data.Responsable = Dto.ResponsableID;

            return await _repository.UpdateGenericAsync(data, true, x => x.Estado, x=>x.Responsable);
        }
        private void VerifyDuplicades(Dictionary<string, string[]> errors, IEnumerable<DocumentoCDTO> lista)
        {
            //Verificamos duplicados por ID
            var dataIds = lista.GroupBy(x => x.DocumentoConID).Select(x => new { ID = x.Key, Count = x.Count() })
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
            var dataNames = lista.GroupBy(x => x.NombreDocumento.Trim()).Select(x => new { Name = x.Key, Count = x.Count() })
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
