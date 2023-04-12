using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TipoDocumento;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface ITipoDocumentoRepository : IBaseGenericRepository<TipoDocumento>, ISearchRepository<TipoDocumentoDTO,SearchBaseTipoDocumentoDTO>
    {
    }
}
