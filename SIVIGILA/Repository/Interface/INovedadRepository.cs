using SIVIGILA.Commons.DTOs.NovedadesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface INovedadRepository: IBaseGenericRepository<Novedades>, ISearchRepository<NovedadesDTO,SearchNovedadesDTO>
    {
    }
}
