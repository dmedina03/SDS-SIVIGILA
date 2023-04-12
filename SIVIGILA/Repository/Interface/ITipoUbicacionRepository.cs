using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface ITipoUbicacionRepository : IBaseGenericRepository<TipoUbicacion>, ISearchRepository<TipoUbicacionDto, SearchTipoUbicacionDTO>
    {

    }
}
