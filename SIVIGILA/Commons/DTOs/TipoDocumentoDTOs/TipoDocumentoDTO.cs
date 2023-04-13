using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.TipoDocumento
{
    public record TipoDocumentoDTO
    {
        public int TipoDocumentoID { get; set; }

        public string NombreDocumento { get; set; }

        public bool TalentoHumano { get; set; }

        public bool Ivc { get; set; }

        public bool Sispic { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
