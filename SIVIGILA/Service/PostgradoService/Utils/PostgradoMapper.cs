using SIVIGILA.Commons.DTOs.PostgradoDto;
using SIVIGILA.Models.Entities;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.PostgradoService.Utils
{
    public static class PostgradoMapper
    {
        public static Postgrado MapToEntity(this PostgradoDTO Dto)
        {
            return new Postgrado()
            {
                Id = Dto.PostgradoID,
                NombrePostgrado = Dto.NombrePostgrado,
                Estado = Dto.Estado,
                ResponsableID = Dto.ReponsableID
            };
        }
        public static IEnumerable<Postgrado> MapToEntity(this IEnumerable<PostgradoDTO> Dto)
        {
            return Dto.Select(x => x.MapToEntity());
        }

        public static IEnumerable<PostgradoDTO> MapToDTO(this IEnumerable<Postgrado> entities)
        {
            return entities.Select(x => new PostgradoDTO
            {
                PostgradoID = x.Id,
                NombrePostgrado = x.NombrePostgrado,
                Estado = x.Estado
            });
        }
    }
}
