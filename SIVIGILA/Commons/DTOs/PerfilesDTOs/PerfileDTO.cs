using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.Perfil
{
    public record PerfilDTO
    {
        public int PerfilID { get; set; }
        public string NombrePerfil { get; set; }
        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
