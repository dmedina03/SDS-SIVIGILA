using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SIVIGILA.Commons.DTOs.Actividad;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.MetaDTOs
{
    public record MetaDTO
    {
        public int MetaId { get; set; }
        public string NombreMeta { get; set; }
        public string DetalleMeta { get; set; }
        public int VigenciaID { get; set; }
        public IEnumerable<ActividadDTO> Actividades { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
