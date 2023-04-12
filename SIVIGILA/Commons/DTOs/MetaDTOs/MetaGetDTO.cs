using SIVIGILA.Commons.DTOs.Actividad;

namespace SIVIGILA.Commons.DTOs.MetaDTOs
{
    public record MetaGetDTO
    {
        public int MetaId { get; set; }
        public string NombreMeta { get; set; }
        public string DetalleMeta { get; set; }
        public int VigenciaID { get; set; }
        public IEnumerable<ActividadGetDTO> Actividades { get; set; }
    }
}
