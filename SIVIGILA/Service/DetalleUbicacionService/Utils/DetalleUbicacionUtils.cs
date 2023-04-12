using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.TipoDatoDto;
using SIVIGILA.Commons.TipoDatoDTO;
using SIVIGILA.Models.Entities;

namespace SIVIGILA.Service.DetalleUbicacionService.Utils
{
    public static class DetalleUbicacionUtils
    {
        public static DetalleUbicacion MapToEntity(this DetalleUbicacionDto Dto)
        {
            return new DetalleUbicacion
            {
                Id = Dto.Id,
                Detalle = Dto.Detalle,
                Estado = Dto.Estado,
                FkTipoUbicacion = Dto.FkTipoUbicacion,
                ResponsableID = Dto.ResponsableID
            };
        }

        public static IEnumerable<DetalleUbicacionGetDTO> MapToDto(this IEnumerable<DetalleUbicacion> entities, bool CopyID = true)
        {
            return entities.Select(x => new DetalleUbicacionGetDTO
            {
                Id = CopyID ? x.Id : 0,
                Detalle = x.Detalle,
                Estado = x.Estado,
                FkTipoUbicacion = x.FkTipoUbicacion
            });
        }
    }
}
