namespace SIVIGILA.Commons.DTOs.Actividad
{
    public record ActividadPatchDTO
    {
        public int id { get; set; }
        public bool estado { get; set; }
        public Guid ResponsableID { get; set; }
    }
}
