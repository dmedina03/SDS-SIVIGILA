using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.Profesion
{
    public record ProfesionDTO
    {
        public int ProfesionID { get; set; }
        public string NombreProfesion { get; set; }
        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
