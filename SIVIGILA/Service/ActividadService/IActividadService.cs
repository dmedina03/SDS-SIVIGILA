using SIVIGILA.Commons.DTOs.Actividad;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.ActividadService
{
    public interface IActividadService :  IExistService,
                                        IUpdateStateGenericService<ActividadPatchDTO>

    {
        public Task<ActividadGetDTO> GetByIdAsync(int id);
        public Task<IEnumerable<ActividadGetDTO>> GetAllByVigenciaIDAsync(int id);
    }
}
