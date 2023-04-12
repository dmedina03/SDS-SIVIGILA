using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.Linea
{
    public record LineaDTO
    {
        public int LineaID { get; set; }
        public string NombreLinea { get; set; }
        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
