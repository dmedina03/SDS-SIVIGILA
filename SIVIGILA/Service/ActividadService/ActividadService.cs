using Microsoft.EntityFrameworkCore;
using SIVIGILA.Commons.DTOs.Actividad;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.MetaService.Utils;

namespace SIVIGILA.Service.ActividadService
{
    public class ActividadService : IActividadService
    {
        private readonly IActividadRepository _repository;

        public ActividadService(IActividadRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExistByIDAsync(int ID)
        {
            return await _repository.ExistGenericAsync(x => x.Id == ID);
        }

        public async Task<IEnumerable<ActividadGetDTO>> GetAllByVigenciaIDAsync(int id)
        {
            var data = await _repository.GenericGetAllAsync(x => x.FkMetaNavigation.FkVigencia == id && x.Estado,
                                                            x => x.Include(c => c.FkMetaNavigation));
            return data.MapToDTO();
        }

        public async Task<ActividadGetDTO> GetByIdAsync(int Id)
        {
            var data = await _repository.GenericGetAsync(x => new ActividadGetDTO
            {
                ActividadID = x.Id,
                NombreActividad = x.NombreActividad,
                DetalleActividad = x.DetalleActividad,
                MetaID = x.FkMeta,
                Estado = x.Estado,
                MetaActividad = x.FkMetaNavigation.NombreMeta + " - " + x.NombreActividad
            }, c => c.Id == Id, x => x.Include(c => c.FkMetaNavigation));

            if (data is null)
                throw new NotFoundException($"No se encontró una Actividad con el ID {Id}");
            return data;
        }

        public async Task<bool> UpdateStateAsync(ActividadPatchDTO Dto)
        {
            var data = await _repository.GenericGetAsync(x => new Actividad
            {
                Id = x.Id,
                Estado = x.Estado,
                Responsable=Dto.ResponsableID
            }, x => x.Id == Dto.id);
            if (data is null)
                throw new NotFoundException($"No se encontró una Actividad con el ID {Dto.id}");
            data.Estado = Dto.estado;
            return await _repository.UpdateGenericAsync(data, true, x => x.Estado,x=>x.Responsable);
        }
    }
}
