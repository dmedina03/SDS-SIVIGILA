using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Models.Entities;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.LineaService.Utils
{
    public static class LineaMapper
    {
        public static Linea MapToEntity(this LineaDTO Dto)
        {
            return new Linea()
            {
                Id = Dto.LineaID,
                NombreLinea = Dto.NombreLinea,
                Estado = Dto.Estado
            };
        }
        public static IEnumerable<Linea> MapToEntity(this IEnumerable<LineaDTO> Dto)
        {
            return Dto.Select(x => new Linea
            {
                Id = x.LineaID,
                NombreLinea = x.NombreLinea,
                Estado = x.Estado
            });
        }

        public static IEnumerable<LineaDTO> MapToDTO(this IEnumerable<Linea> entities)
        {
            return entities.Select(x => new LineaDTO
            {
                LineaID = x.Id,
                NombreLinea = x.NombreLinea,
                Estado = x.Estado
            });
        }
    }
}
