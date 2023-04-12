using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.Vigencia
{
    public record VigenciaSimpleGetDTO
    {
        public int VigenciaID { get; set; } = 0;
        public string Presupuesto { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTime _fechaInicio { get; set; }
        public string FechaInicio
        {
            get
            {
                return _fechaInicio.ToString("dd/MM/yyyy");
            }
        }
        [DataType(DataType.Date)]
        [JsonIgnore]
        public DateTime _fechaFin { get; set; }
        public string FechaFin
        {
            get
            {
                return _fechaFin.ToString("dd/MM/yyyy");
            }
        }
        public bool AdicionarTiemposParaNovedades
        {
            get
            {
                return _adicionTiempo != null;
            }
        }
        [JsonIgnore]
        public DateTime? _adicionTiempo { get; set; }
        public string? AdicionTiempo 
        {
            get
            {
                return _adicionTiempo?.ToString("dd/MM/yyyy") ?? null;
            } 
        }
        public bool Disponible { get; set; }
        public string Nombre 
        {
            get
            {
                return Presupuesto + " - " + _fechaInicio.ToString("dd MMMM yyyy") + " - " + _fechaFin.ToString("dd MMMM yyyy");
            }
        }
        public EstadoVigenciaDTO Estado { get; set; }
    }
}
