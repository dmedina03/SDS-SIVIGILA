using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Models.Entities;

namespace SIVIGILA.Service.DpCondiDiscapaService.Utils
{
    public static class DpCondiDiscapaMapper
    {

        public static DpCondiDiscapa MapToEntity(this DpCondiDiscapaDTO Dto)
        {
            return new DpCondiDiscapa
            {
                Id = Dto.ID,
                Descripcion = Dto.Descripcion,
                TalentoHumano = Dto.TalentoHumano,
                Ivc = Dto.Ivc,
                Vsa = Dto.Vsa,
                ResponsableID = Dto.ResponsableID
            };
        }
        public static IEnumerable<DpCondiDiscapa> MapToEntity(this IEnumerable<DpCondiDiscapaDTO> Dtos)
        {
            return Dtos.Select(x => new DpCondiDiscapa
            {
                Id = x.ID,
                Descripcion = x.Descripcion,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
                Vsa= x.Vsa,
                ResponsableID = x.ResponsableID
            });
        }

        public static IEnumerable<DpCondiDiscapaDTO> MapToDTO(this IEnumerable<DpCondiDiscapa> entities)
        {
            return entities.Select(x => new DpCondiDiscapaDTO
            {
                ID = x.Id,
                Descripcion = x.Descripcion,
                Ivc = x.Ivc,
                TalentoHumano = x.TalentoHumano,
                Vsa = x.Vsa
            });
        }




    }
}
