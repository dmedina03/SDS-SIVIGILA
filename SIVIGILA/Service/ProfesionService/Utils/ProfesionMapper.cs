using SIVIGILA.Commons.DTOs.Profesion;
using SIVIGILA.Models.Entities;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.ProfesionService.Utils
{
    public static class ProfesionMapper
    {
        public static Profesion MapToEntity(this ProfesionDTO Dto)
        {
            return new Profesion()
            {
                Id = Dto.ProfesionID,
                NombreProfesion = Dto.NombreProfesion,
                Estado = Dto.Estado,
                ReponsableID = Dto.ResponsableID
            };
        }
        public static IEnumerable<Profesion> MapToEntity(this IEnumerable<ProfesionDTO> Dto)
        {
            return Dto.Select(x => x.MapToEntity());
        }

        public static IEnumerable<ProfesionDTO> MapToDTO(this IEnumerable<Profesion> entities)
        {
            return entities.Select(x => new ProfesionDTO
            {
                ProfesionID = x.Id,
                NombreProfesion = x.NombreProfesion,
                Estado = x.Estado
            });
        }
    }
}
