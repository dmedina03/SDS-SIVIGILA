using System.ComponentModel.DataAnnotations;

namespace SIVIGILA.Commons.DTOs.Vigencia
{
    public record VigenciaSimpleDTO
    {
        public int VigenciaID { get; set; }
        public string Presupuesto { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
        public int EstadoID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? AdicionTiempo { get; set; }
        public bool Disponible { get; set; }
    }
}
