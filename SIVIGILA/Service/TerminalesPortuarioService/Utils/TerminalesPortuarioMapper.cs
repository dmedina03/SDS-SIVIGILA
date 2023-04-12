using SIVIGILA.Commons.DTOs.TerminalesPortuario;
using SIVIGILA.Models.Entities;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.TerminalesPortuarioService.Utils
{
    public static class TerminalesPortuarioMapper
    {
        public static TerminalesPortuario MapToEntity(this TerminalesPortuarioDTO Dto)
        {
            return new TerminalesPortuario()
            {
                Id = Dto.TerminalesPortuarioID,
                NombreTerminalesPortuario = Dto.NombreTerminalesPortuario,
                Estado = Dto.Estado,
                Responsable = Dto.ResponsableID
            };
        }
        public static IEnumerable<TerminalesPortuario> MapToEntity(this IEnumerable<TerminalesPortuarioDTO> Dto)
        {
            return Dto.Select(x => new TerminalesPortuario
            {
                Id = x.TerminalesPortuarioID,
                NombreTerminalesPortuario = x.NombreTerminalesPortuario,
                Estado = x.Estado
            });
        }

        public static IEnumerable<TerminalesPortuarioDTO> MapToDTO(this IEnumerable<TerminalesPortuario> entities)
        {
            return entities.Select(x => new TerminalesPortuarioDTO
            {
                TerminalesPortuarioID = x.Id,
                NombreTerminalesPortuario = x.NombreTerminalesPortuario,
                Estado = x.Estado
            });
        }
    }
}
