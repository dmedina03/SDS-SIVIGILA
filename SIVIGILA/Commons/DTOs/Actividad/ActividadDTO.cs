using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.Actividad
{
    public record ActividadDTO
    {
        public int ActividadID { get; set; }
        public string NombreActividad { get; set; }
        public string DetalleActividad { get; set; }
        public bool Estado { get; set; }
        public int MetaID { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
