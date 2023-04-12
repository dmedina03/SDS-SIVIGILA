using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.NovedadesDTOs
{
    public class NovedadesDTO
    {
        public int NovedadesID { get; set; }
        public string NombreNovedad { get; set; }
        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
