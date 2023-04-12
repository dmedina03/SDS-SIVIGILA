namespace SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs
{
    public record ProductoVigenciaPatchDTO
    {
        public int id { get; set; }
        public bool estado { get; set; }
        public Guid ResponsableID { get; set; }
    }
}
