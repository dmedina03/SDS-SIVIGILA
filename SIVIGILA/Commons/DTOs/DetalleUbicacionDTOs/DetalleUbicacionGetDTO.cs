namespace SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs
{
    public record DetalleUbicacionGetDTO
    {
        public int Id { get; set; }

        public int? FkTipoUbicacion { get; set; }
        public string? Detalle { get; set; }

        public bool? Estado { get; set; }
    }
}
