using SIVIGILA.Commons.DTOs.TipoDocumento;
using SIVIGILA.Models.Entities;

namespace SIVIGILA.Service.TipoDocumentoService.Utils
{
    public static class TipoDocumentoMapper
    {

        public static TipoDocumento MapToEntity(this TipoDocumentoDTO Dto)
        {
            return new TipoDocumento()
            {
                Id = Dto.TipoDocumentoID,
                NombreDocumento = Dto.NombreDocumento,
                TalentoHumano = Dto.TalentoHumano,
                Ivc = Dto.Ivc,
                Sispic = Dto.Sispic,
                Responsable = Dto.ResponsableID
            };
        }
        public static IEnumerable<TipoDocumento> MapToEntity(this IEnumerable<TipoDocumentoDTO> Dto)
        {
            return Dto.Select(x => new TipoDocumento
            {
                Id = x.TipoDocumentoID,
                NombreDocumento = x.NombreDocumento,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
                Sispic = x.Sispic,
            });
        }

        public static IEnumerable<TipoDocumentoDTO> MapToDTO(this IEnumerable<TipoDocumento> entities)
        {
            return entities.Select(x => new TipoDocumentoDTO
            {
                TipoDocumentoID = x.Id,
                NombreDocumento = x.NombreDocumento,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
                Sispic = x.Sispic

            });
        }
    }
}
