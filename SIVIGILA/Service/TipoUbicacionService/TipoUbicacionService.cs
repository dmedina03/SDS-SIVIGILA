using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Repository.Interface.BaseInterface;
using SIVIGILA.Service.DetalleUbicacionService;
using SIVIGILA.Service.TipoUbicacionService.Utils;

namespace SIVIGILA.Service.TipoUbicacionService
{
    public class TipoUbicacionService : ITipoUbicacionService
    {
        private readonly ITipoUbicacionRepository _tipoUbicacionRepository;
        private readonly IDetalleUbicacionService _detalleUbicacionService;
        private readonly IValidator<TipoUbicacionDto> _validatorTipoUbi;
        public TipoUbicacionService(ITipoUbicacionRepository tipoUbicacionRepository,
            IDetalleUbicacionService detalleUbicacionService, IValidator<TipoUbicacionDto> validatorTipoUbi)
        {
            _tipoUbicacionRepository = tipoUbicacionRepository;
            _detalleUbicacionService = detalleUbicacionService;
            _validatorTipoUbi = validatorTipoUbi;
        }

        public async Task<int> AddAsync(TipoUbicacionDto tipoUbicacion)
        {
            var result = await _validatorTipoUbi.ValidateAsync(tipoUbicacion, opts => opts.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }
            var entity = tipoUbicacion.MapToEntity();
            bool IsComplete = await _tipoUbicacionRepository.AddAsync(entity);
            return entity.Id;
        }
        public async Task<bool> UpdateAsync(TipoUbicacionDto Dto)
        {
            //Revisar la validacion de lista desplegable y valor unico de TIPO DATO
            var resultTipoUbi = await _validatorTipoUbi.ValidateAsync(Dto, opts => opts.IncludeRuleSets("Update", "Any"));
            if (!resultTipoUbi.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                resultTipoUbi.ToDictionary());
            }
            var entity = Dto.MapToEntity();
            return await _tipoUbicacionRepository.UpdateGenericAsync(entity);
        }

        public async Task<IEnumerable<TipoUbicacionGetDTO>> GetAllAsync()
        {
            var data = await _tipoUbicacionRepository.GenericGetAllAsync(x => x.Estado, x => x.Include(c => c.DetalleUbicacions));
            return data.MapToDto();
        }

        public async Task<bool> CreateOrUpdateRangeAsync(IEnumerable<TipoUbicacionDto> listDto)
        {
            Dictionary<string, string[]> dataValidationError = new();
            ValidationResult result;
            int i = 0;
            VerifyDuplicades(dataValidationError, listDto);

            foreach (var dto in listDto)
            {
                result = await _validatorTipoUbi.ValidateAsync(dto, opt => opt.IncludeRuleSets("Any"));
                if (!result.IsValid)
                {
                    foreach (var elemet in result.ToDictionary())
                    {
                        dataValidationError.Add($"TipoUbicacionDTO[{i}].{elemet.Key}", elemet.Value);
                    }
                }
                i++;
            }
            if (dataValidationError.Count() > 0)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                dataValidationError);
            }
            var entities = listDto.MapToEntity();
            List<Task<bool>> tasks = new();

            //Se eseleccionan los valores a actualizar
            var toUpdate = entities.Where(x => x.Id != 0);
            if (toUpdate.Count() != 0)
                tasks.Add(_tipoUbicacionRepository.UpdateRangeAsync(toUpdate, false));
            var toCreate = entities.Where(x => x.Id == 0);
            if (toCreate.Count() != 0)
                tasks.Add(_tipoUbicacionRepository.AddRangeAsync(toCreate, false));
            await _tipoUbicacionRepository.SaveChangesAsync();
            return true;
        }

        private void VerifyDuplicades(Dictionary<string, string[]> errors, IEnumerable<TipoUbicacionDto> lista)
        {
            //Verificamos duplicados por ID
            var dataIds = lista.GroupBy(x => x.Id).Select(x => new { ID = x.Key, Count = x.Count() })
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

        public async Task<DataCollection<TipoUbicacionDto>> GetByParamsAsync(SearchTipoUbicacionDTO Dto)
        {
            return await _tipoUbicacionRepository.GetByParamsAsync(Dto);
        }

        public async Task<TipoUbicacionGetDTO> GetByIdAsync(int Id)
        {
            var data = await _tipoUbicacionRepository.GenericGetAsync(x => new TipoUbicacionGetDTO
            {
                Id = x.Id,
                Estado = x.Estado,
                Ubicacion = x.Ubicacion,
                FkTipoDato = x.FkTipoDato,
                ValoresListaUbicacion = x.DetalleUbicacions.Select(c => new DetalleUbicacionGetDTO
                {
                    Id = c.Id,
                    Detalle = c.Detalle,
                    FkTipoUbicacion = c.FkTipoUbicacion
                })
            }, x => x.Id == Id);
            if (data is null)
                throw new NotFoundException($"No se encontró un Tipo de Ubicacion con el ID {Id}");
            return data;
        }
    }
}
