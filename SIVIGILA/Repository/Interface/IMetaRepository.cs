using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IMetaRepository: IBaseGenericRepository<Meta>,ISearchRepository<MetaGetDTO,SearchMetaDTO>
    {
    }
}
