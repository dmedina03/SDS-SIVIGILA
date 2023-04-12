using MessagePack;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs
{
    public class DetalleUbicacionDto
    {
        public int Id { get; set; }

        public int FkTipoUbicacion { get; set; }
        public string Detalle { get; set; }

        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }

}
