using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Models.Entities;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.DpOrientSexualServices.Utils
{
    public static class DpOrientSexualMapper
    {

        public static DpOrientSexual MapToEntity(this DpOrientSexualDTO Dto)
        {
            return new DpOrientSexual()
            {
                Id = Dto.ID,
                Descripcion = Dto.Descripcion,
                Ivc = Dto.Ivc,
                TalentoHumano = Dto.TalentoHumano,
                Vsa = Dto.Vsa,
                RespomsableID = Dto.RespomsableID
            };
        }
        public static IEnumerable<DpOrientSexual> MapToEntity(this IEnumerable<DpOrientSexualDTO> dtos)
        {
            return dtos.Select(Dto => new DpOrientSexual
            {
                Id = Dto.ID,
                Descripcion = Dto.Descripcion,
                Ivc = Dto.Ivc,
                TalentoHumano = Dto.TalentoHumano,
                Vsa = Dto.Vsa,
                RespomsableID = Dto.RespomsableID
            });
        }
        public static IEnumerable<DpOrientSexualDTO> MapToDto(this IEnumerable<DpOrientSexual> entities)
        {
            return entities.Select(Dto => new DpOrientSexualDTO
            {
                ID = Dto.Id,
                Descripcion = Dto.Descripcion,
                Ivc = Dto.Ivc,
                TalentoHumano = Dto.TalentoHumano,
                Vsa = Dto.Vsa,
                RespomsableID = Dto.RespomsableID
            });
        }

    }
}
