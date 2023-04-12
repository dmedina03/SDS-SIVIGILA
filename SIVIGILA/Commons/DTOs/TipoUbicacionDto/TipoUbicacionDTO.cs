using SIVIGILA.Models.Entities;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.TipoUbicacionDto
{
    public record TipoUbicacionDto 
    {
        public int Id { get; set; }

        public string Ubicacion { get; set; }

        public int FkTipoDato { get; set; }

        public bool Estado { get; set; }

        public List<DetalleUbicacionDto>? ValoresListaUbicacion { get; set; }

        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }


}
