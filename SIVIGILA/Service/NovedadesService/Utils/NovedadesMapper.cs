using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.NovedadesDTOs;
using SIVIGILA.Models.Entities;

namespace SIVIGILA.Service.NovedadesService.Utils
{
    public static class NovedadesMapper
    {
        public static Novedades MapToEntity(this NovedadesDTO Dto)
        {
            return new Novedades()
            {
                Id = Dto.NovedadesID,
                NombreNovedad = Dto.NombreNovedad,
                Estado = Dto.Estado,
                Responsable=Dto.ResponsableID
            };
        }
        public static IEnumerable<Novedades> MapToEntity(this IEnumerable<NovedadesDTO> Dto)
        {
            return Dto.Select(x => new Novedades
            {
                Id = x.NovedadesID,
                NombreNovedad = x.NombreNovedad,
                Estado = x.Estado,
                Responsable=x.ResponsableID
            });
        }

        public static IEnumerable<NovedadesDTO> MapToDTO(this IEnumerable<Novedades> entities)
        {
            return entities.Select(x => new NovedadesDTO
            {
                NovedadesID = x.Id,
                NombreNovedad = x.NombreNovedad,
                Estado = x.Estado
            });
        }
    }
}
