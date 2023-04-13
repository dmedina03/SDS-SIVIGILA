namespace SIVIGILA.Commons.DTOs.Vigencia
{
    public record EstadoVigenciaDTO
    {
        public int EstadoID { get; set; }
        public string Descripcion { get; set; }= string.Empty;
    }
}
