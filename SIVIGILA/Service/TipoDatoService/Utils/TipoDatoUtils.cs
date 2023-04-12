using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.TipoDatoDto;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.TipoDatoDTO;
using SIVIGILA.Models.Entities;
using System.Collections.ObjectModel;

namespace SIVIGILA.Service.TipoDatoService.Utils
{
    public static class TipoDatoUtils
    {
        public static TipoDato MapToEntity(this TipoDatoDTO Dto)
        {
            return new TipoDato
            {
                Id = Dto.Id,
                Descripcion = Dto.Descripcion
            };
        }

        public static IEnumerable<TipoDatoGetDTO> MapToDto(this IEnumerable<TipoDato> entities, bool CopyID = true)
        {
            return entities.Select(x => new TipoDatoGetDTO
            {
                Id = CopyID ? x.Id : 0,
                Descripcion = x.Descripcion
            });
        }
    }
}
