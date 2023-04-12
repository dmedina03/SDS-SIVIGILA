using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;

namespace SIVIGILA.Commons.DTOs.TipoUbicacionDto
{
    public record TipoUbicacionGetDTO
    {
        public int Id { get; set; }

        public string? Ubicacion { get; set; }

        public int? FkTipoDato { get; set; }

        public bool? Estado { get; set; }

        public IEnumerable<DetalleUbicacionGetDTO> ValoresListaUbicacion { get; set; }
    }
}
