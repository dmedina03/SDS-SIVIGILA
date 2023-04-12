using SIVIGILA.Commons.DTOs.Actividad;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Models.Entities;
using System.Collections.ObjectModel;

namespace SIVIGILA.Service.TipoUbicacionService.Utils
{
    public static class TipoUbicacionUtils
    {
        public static IEnumerable<DetalleUbicacion> MapToEntity(this IEnumerable<DetalleUbicacionDto> Dtos)
        {
            return Dtos.Select(Dto => new DetalleUbicacion
            {
                Id = Dto.Id,
                Detalle =Dto.Detalle,
                Estado = Dto.Estado,
                FkTipoUbicacion = Dto.FkTipoUbicacion,
                ResponsableID= Dto.ResponsableID
            });
        }
        public static TipoUbicacion MapToEntity(this TipoUbicacionDto Dto)
        {
            return new TipoUbicacion
            {
                Id = Dto.Id,
                Ubicacion = Dto.Ubicacion,
                FkTipoDato = Dto.FkTipoDato,
                Estado = Dto.Estado,
                DetalleUbicacions = new Collection<DetalleUbicacion>(Dto.ValoresListaUbicacion?.MapToEntity().ToList() ?? new()),
                ResponsableCambio = Dto.ResponsableID
            };
        }
        public static IEnumerable<TipoUbicacion> MapToEntity(this IEnumerable<TipoUbicacionDto> Dto)
        {
            return Dto.Select(x => x.MapToEntity());
        }

        public static IEnumerable<TipoUbicacionGetDTO> MapToDto(this IEnumerable<TipoUbicacion> entities, bool CopyID = true)
        {
            return entities.Select(x => new TipoUbicacionGetDTO
            {
                Id = CopyID ? x.Id : 0,
                Estado = x.Estado,
                Ubicacion = x.Ubicacion,
                FkTipoDato = x.FkTipoDato,
                ValoresListaUbicacion = x.DetalleUbicacions.Select(c => new DetalleUbicacionGetDTO
                {
                    Id = CopyID ? c.Id : 0,
                    Detalle = c.Detalle,
                    Estado = c.Estado,
                    FkTipoUbicacion = CopyID ? c.FkTipoUbicacion:0
                })
            });
        }

    }
}
