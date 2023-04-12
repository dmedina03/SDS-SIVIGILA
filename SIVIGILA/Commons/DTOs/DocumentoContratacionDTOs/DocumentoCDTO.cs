using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs
{
    public record DocumentoCDTO
    {
        public int DocumentoConID { get; set; }
        public string NombreDocumento { get; set; }
        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
