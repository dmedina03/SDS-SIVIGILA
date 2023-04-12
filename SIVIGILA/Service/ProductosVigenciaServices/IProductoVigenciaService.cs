using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.ProductosVigenciaServices
{
    public interface IProductoVigenciaService: ICreateOrUpdateRangeService<ProductosVigenciaDTO>, ICreateService<ProductosVigenciaDTO>,
                                                IUpdateService<ProductosVigenciaDTO>, IGetService<ProductosVigenciaDTO, ProductosVigenciaDTO>,
                                                IUpdateStateGenericService<ProductoVigenciaPatchDTO>, IExistService,
                                                IServiceSearch<ProductosVigenciaDTO, SearchProductoVigenciaDTO>,
                                                IGetAllNamesService
    {
        public Task<bool> ExistByNameAsync(string nombreLinea);
    }
}
