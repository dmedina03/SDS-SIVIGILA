using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.TerminalesPortuario
{
    public record TerminalesPortuarioDTO
    {
        public int TerminalesPortuarioID { get; set; }
        public string NombreTerminalesPortuario { get; set; }
        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
