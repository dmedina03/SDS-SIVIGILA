using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.Vigencia;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.VigenciaService.Utils;

namespace SIVIGILA.Service.VigenciaService
{
    public class VigenciaService : IVigenciaService
    {
        private readonly IVIgenciaRepository _repository;
        private readonly IEstadoRepository _estadoRepository;
        private readonly IVIgenciaRepository _vigenciaRepository;
        private readonly IValidator<VigenciaDTO> _validator;

        public VigenciaService(IVIgenciaRepository repository, IEstadoRepository estadoRepository, IVIgenciaRepository vigenciaRepository, IValidator<VigenciaDTO> validator)
        {
            _repository = repository;
            _estadoRepository = estadoRepository;
            _vigenciaRepository = vigenciaRepository;
            _validator = validator;
        }

        public async Task<int> AddAsync(VigenciaDTO dto)
        {
            var result = await _validator.ValidateAsync(dto, opt => opt.IncludeRuleSets("Create", "Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }

            var entity = dto.MapToEntity();
            bool IsComplete = await _repository.AddAsync(entity);
            return entity.VigenciaID;
        }

        public async Task<bool> UpdateAsync(VigenciaDTO dto)
        {
            var result = await _validator.ValidateAsync(dto, opt => opt.IncludeRuleSets("Any"));
            if (!result.IsValid)
            {
                throw new ValidationDataException("Se encontrarón errores de validación",
                                result.ToDictionary());
            }

            var entity = dto.MapToEntity();
            bool IsComplete = await _repository.UpdateGenericAsync(entity);
            return IsComplete;
        }

        public async Task<bool> CreateOrUpdateRangeAsync(IEnumerable<VigenciaDTO> Dtos)
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
            var entitys = Dtos.MapToEntity();
            List<Task<bool>> tasks = new();
            //Se seleccionan las que se van a actualizar
            var toUpdate = entitys.Where(x => x.VigenciaID != 0);
            if (toUpdate.Count() != 0)
                tasks.Add(_repository.UpdateRangeAsync(toUpdate, false));
            var toCreate = entitys.Where(x => x.VigenciaID == 0);
            if (toCreate.Count() != 0)
                tasks.Add(_repository.AddRangeAsync(toCreate, false));
            var dataComplete = await Task.WhenAll(tasks);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<DataCollection<VigenciaGetDTO>> GetByParamsAsync(SearchVigenciaDTO Dto)
        {
            var data = await _repository.GetByParamsAsync(Dto);
            return data;
        }

        public async Task<IEnumerable<VigenciaShortInfoDTO>> GetShortInfoActiveDTO()
        {
            var data = (await _repository.GenericGetAllAsync(x => x.Estado, null, null,
                        x => new Models.Entities.Vigencia
                        {
                            VigenciaID = x.VigenciaID,
                            Presupuesto = x.Presupuesto,
                            FechaInicio = x.FechaInicio,
                            FechaFin = x.FechaFin,
                        })).Select(x => new VigenciaShortInfoDTO
                        {
                            VigenciaID = x.VigenciaID,
                            NombreVigencia = x.Presupuesto + " - " + x.FechaInicio.ToString("dd MMMM yyyy") +
                                " - " + x.FechaFin.ToString("dd MMMM yyyy")
                        });
            return data;
        }

        public async Task<VigenciaGetDTO> GetByIdAsync(int Id)
        {
            var data = await _repository.GenericGetAsync(x => new VigenciaGetDTO
            {
                VigenciaID = x.VigenciaID,
                Presupuesto = x.Presupuesto,
                _fechaInicio = x.FechaInicio.Date,
                _fechaFin = x.FechaFin,
                Estado = new EstadoVigenciaDTO
                {
                    EstadoID = x.Estado_Vigencia_ID,
                    Descripcion = x.EstadoVigencia.Descripcion
                },
                _adicionTiempo = x.AdicionTiempo,
                Disponible = x.Estado,
                VigenciasAdicionales = x.VigenciasAdicionales.Select(c => new VigenciaSimpleGetDTO
                {
                    VigenciaID = c.VigenciaID,
                    Presupuesto = c.Presupuesto,
                    _fechaInicio = c.FechaInicio.Date,
                    _fechaFin = c.FechaFin,
                    Estado = new EstadoVigenciaDTO
                    {
                        EstadoID = c.Estado_Vigencia_ID,
                        Descripcion = c.EstadoVigencia.Descripcion
                    },
                    _adicionTiempo = c.AdicionTiempo,
                    Disponible = c.Estado,
                }).ToList()
            }, x => x.VigenciaID == Id, x => x.Include(c => c.VigenciasAdicionales).Include(c=>c.EstadoVigencia));

            if (data is null)
                throw new NotFoundException($"No Vigencia associated with the ID {Id} was found");
            return data;
        }

        public async Task<IEnumerable<EstadoVigenciaDTO>> GetStatesVigenciaAsync()
        {
            var data = (await _estadoRepository.GenericGetAllAsync(x => x.Tipo == "Vigencia",
                null, null, x => new Models.Entities.Estado
                {
                    Estado_ID = x.Estado_ID,
                    Descripcion = x.Descripcion
                })).Select(x=>new EstadoVigenciaDTO
                {
                    EstadoID=x.Estado_ID,
                    Descripcion=x.Descripcion
                });
            return data;
        }

        public async Task<IEnumerable<VigenciaSimpleGetDTO>> GetAllAsync()
        {
            var data = await _repository.
                GenericGetAllAsync(x => x.Estado, x => x.Include(c => c.EstadoVigencia));
            return data.MapToDTO();
        }

        //Verificación de registros Duplicados
        private void VerifyDuplicades(Dictionary<string, string[]> errors, IEnumerable<VigenciaDTO> lista)
        {
            //Verificamos duplicados por ID
            var dataIds = lista.GroupBy(x => x.VigenciaID).Select(x => new { ID = x.Key, Count = x.Count() })
                        .Where(x => x.Count > 1 && x.ID != 0);
            var errores = new List<string>();
            if (dataIds.Count() > 0)
            {
                foreach (var item in dataIds)
                {
                    errores.Add($"El Dto con el Id {item.ID} se repite {item.Count} veces en el JSON");
                }
            }
        }
    }
}
