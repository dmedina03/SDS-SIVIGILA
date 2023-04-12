using SIVIGILA.Commons.DTOs.Perfil;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IPerfilVigenciaRepository : IBaseGenericRepository<PerfilVigencia>, ISearchRepository<PerfilVigenciaGetDTO, SearchPerfilVigenciaDTO>
    {
    }
}
