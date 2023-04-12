using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs
{
    public record class DpSexoDTO
    {
        public int ID { get; set; }
        public string? Descripcion { get; set; }
        public bool TalentoHumano { get; set; }
        public bool Ivc { get; set; }
        public bool Vsa { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
