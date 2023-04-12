using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs
{
    public class DpPresenEtnicaGetDTO
    {
        public int ID { get; set; }
        public string? Descripcion { get; set; }
        public bool? TalentoHumano { get; set; }
        public bool? Ivc { get; set; }
        public bool? Vsa { get; set; }
    }
}
