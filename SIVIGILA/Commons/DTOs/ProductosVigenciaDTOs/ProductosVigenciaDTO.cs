using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs
{
    public class ProductosVigenciaDTO
    {
        public int ProductoVigenciaID { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public bool Estado { get; set; }
        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }
}
