using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs;
using SIVIGILA.Models.Entities;

namespace SIVIGILA.Service.ProductosVigenciaServices.Utils
{
    public static class ProductosVigenciaMapper
    {
        public static ProductosVigencia MapToEntity(this ProductosVigenciaDTO Dto)
        {
            return new ProductosVigencia()
            {
                Id = Dto.ProductoVigenciaID,
                NombreProducto = Dto.NombreProducto,
                UnidadMedida= Dto.UnidadMedida,
                Estado = Dto.Estado,
                Responsable= Dto.ResponsableID
            };
        }
        public static IEnumerable<ProductosVigencia> MapToEntity(this IEnumerable<ProductosVigenciaDTO> Dto)
        {
            return Dto.Select(x => new ProductosVigencia()
            {
                Id = x.ProductoVigenciaID,
                NombreProducto = x.NombreProducto,
                UnidadMedida = x.UnidadMedida,
                Estado = x.Estado,
                Responsable=x.ResponsableID
            });
        }

        public static IEnumerable<ProductosVigenciaDTO> MapToDTO(this IEnumerable<ProductosVigencia> entities)
        {
            return entities.Select(x => new ProductosVigenciaDTO()
            {
                ProductoVigenciaID = x.Id,
                NombreProducto = x.NombreProducto,
                UnidadMedida = x.UnidadMedida,
                Estado = x.Estado
            });
        }
    }
}
