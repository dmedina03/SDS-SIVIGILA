using SIVIGILA.Commons.DTOs.PostgradoDto;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IPostgradoRepository: IBaseGenericRepository<Postgrado>,ISearchRepository<PostgradoDTO, SearchPostgradoDTO>
    {
    }
}
