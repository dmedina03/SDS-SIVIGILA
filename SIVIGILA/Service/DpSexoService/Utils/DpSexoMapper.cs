using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Models.Entities;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.DpSexoServices.Utils
{
    public static class DpSexoMapper
    {

        public static DpSexo MapToEntity(this DpSexoDTO Dto)
        {
            return new DpSexo()
            {
                Id = Dto.ID,
                Descripcion = Dto.Descripcion,
                Ivc = Dto.Ivc,
                TalentoHumano = Dto.TalentoHumano,
                Vsa = Dto.Vsa,
                ResponsableID = Dto.ResponsableID
            };
        }
        public static IEnumerable<DpSexo> MapToEntity(this IEnumerable<DpSexoDTO> dtos)
        {
            return dtos.Select(Dto => new DpSexo
            {
                Id = Dto.ID,
                Descripcion = Dto.Descripcion,
                Ivc = Dto.Ivc,
                TalentoHumano = Dto.TalentoHumano,
                Vsa = Dto.Vsa,
                ResponsableID = Dto.ResponsableID
            });
        }
        public static IEnumerable<DpSexoDTO> MapToDto(this IEnumerable<DpSexo> entities)
        {
            return entities.Select(Dto => new DpSexoDTO
            {
                ID = Dto.Id,
                Descripcion = Dto.Descripcion,
                Ivc = Dto.Ivc,
                TalentoHumano = Dto.TalentoHumano,
                Vsa = Dto.Vsa,
                ResponsableID = Dto.ResponsableID
            });
        }

    }
}
