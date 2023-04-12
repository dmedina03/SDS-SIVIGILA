using SIVIGILA.Commons.DTOs.Perfil;
using SIVIGILA.Models.Entities;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.PerfilService.Utils
{
    public static class PerfilMapper
    {
        public static Perfil MapToEntity(this PerfilDTO Dto)
        {
            return new Perfil()
            {
                Id = Dto.PerfilID,
                NombrePerfil = Dto.NombrePerfil,
                Estado = Dto.Estado,
                ResponsableID = Dto.ResponsableID
            };
        }
        public static IEnumerable<Perfil> MapToEntity(this IEnumerable<PerfilDTO> Dto)
        {
            return Dto.Select(x => x.MapToEntity());
        }

        public static IEnumerable<PerfilDTO> MapToDTO(this IEnumerable<Perfil> entities)
        {
            return entities.Select(x => new PerfilDTO
            {
                PerfilID = x.Id,
                NombrePerfil = x.NombrePerfil,
                Estado = x.Estado
            });
        }
    }
}
