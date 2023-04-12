using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IDetalleUbicacionRepository : IBaseGenericRepository<DetalleUbicacion>
    {
        public Task<IEnumerable<DetalleUbicacionDto>> GetDetalleUbicacinoByTipoUbicacion(int idTipoUbi);

    }
}
