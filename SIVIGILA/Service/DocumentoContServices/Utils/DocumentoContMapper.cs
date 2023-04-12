using SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository;

namespace SIVIGILA.Service.DocumentoContServices.Utils
{
    public static class DocumentoContMapper
    {
        public static DocumentacionContratacion MapToEntity(this DocumentoCDTO Dto)
        {
            return new DocumentacionContratacion()
            {
                Id = Dto.DocumentoConID,
                NombreDocumento = Dto.NombreDocumento,
                Estado = Dto.Estado,
                Responsable=Dto.ResponsableID
            };
        }
        public static IEnumerable<DocumentacionContratacion> MapToEntity(this IEnumerable<DocumentoCDTO> Dto)
        {
            return Dto.Select(x => new DocumentacionContratacion
            {
                Id = x.DocumentoConID,
                NombreDocumento = x.NombreDocumento,
                Estado = x.Estado,
                Responsable = x.ResponsableID
            });
        }

        public static IEnumerable<DocumentoCDTO> MapToDTO(this IEnumerable<DocumentacionContratacion> entities)
        {
            return entities.Select(x => new DocumentoCDTO
            {
                DocumentoConID=x.Id,
                NombreDocumento=x.NombreDocumento,
                Estado=x.Estado
            });
        }
    }
}
