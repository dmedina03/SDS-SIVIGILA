using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.PostgradoDto
{
    public record PostgradoDTO
    {
        public int PostgradoID { get; set; }

        public string NombrePostgrado { get; set; }

        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ReponsableID { get; set; }
    }
}
