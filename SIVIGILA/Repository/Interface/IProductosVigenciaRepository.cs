using SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IProductosVigenciaRepository : IBaseGenericRepository<ProductosVigencia>,
                                                    ISearchRepository<ProductosVigenciaDTO,SearchProductoVigenciaDTO>
    {
    }
}
