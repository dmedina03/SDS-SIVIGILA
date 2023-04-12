using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Models.Entities;

namespace SIVIGILA.Service.DpPresenEtnicaServices.Utils
{
    public static class DpPresenEtnicaMapper
    {

        public static DpPresenEtnica MapToEntity(this DpPresenEtnicaDTO Dto)
        {
            return new DpPresenEtnica()
            {
                Id = Dto.ID,
                Descripcion = Dto.Descripcion,
                Ivc = Dto.Ivc,
                TalentoHumano = Dto.TalentoHumano,
                Vsa = Dto.Vsa,
                ResponsableID = Dto.ResponsableID
            };
        }
        public static IEnumerable<DpPresenEtnica> MapToEntity(this IEnumerable<DpPresenEtnicaDTO> entitites)
        {
            return entitites.Select(x => new DpPresenEtnica
            {
                Id = x.ID,
                Descripcion = x.Descripcion,
                Ivc = x.Ivc,
                TalentoHumano = x.TalentoHumano,
                Vsa = x.Vsa,
                ResponsableID = x.ResponsableID
            });
        }
        public static IEnumerable<DpPresenEtnicaGetDTO> MapToDTO(this IEnumerable<DpPresenEtnica> entitites)
        {
            return entitites.Select(x => new DpPresenEtnicaGetDTO
            {
                ID = x.Id,
                Descripcion = x.Descripcion,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
                Vsa = x.Vsa
            });
        }

    }
}
