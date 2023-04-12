using SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class ProductosVigenciaRepository : BaseRepository<ProductosVigencia>, IProductosVigenciaRepository
    {
        public ProductosVigenciaRepository(context context): base(context)
        {

        }

        public async Task<DataCollection<ProductosVigenciaDTO>> GetByParamsAsync(SearchProductoVigenciaDTO parameters)
        {
            var data = Entity.
                Select(x=>new ProductosVigenciaDTO
                {
                    ProductoVigenciaID=x.Id,
                    NombreProducto=x.NombreProducto,
                    UnidadMedida=x.UnidadMedida,
                    Estado=x.Estado
                }).
                AsQueryable();
            if(!String.IsNullOrEmpty(parameters.NombreProducto))
            {
                data = data.Where(x => x.NombreProducto.ToLower().Trim().Contains(parameters.NombreProducto));
            }

            return await data.GetPagedAsync(parameters.page, parameters.take);

        }
    }
}
