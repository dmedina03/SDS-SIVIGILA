using SIVIGILA.Commons.DTOs.Perfil;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IPerfilRepository : IBaseGenericRepository<Perfil>, ISearchRepository<PerfilDTO, SearchPerfilDTO>
    {
    }
}
