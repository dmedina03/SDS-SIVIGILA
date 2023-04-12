namespace SIVIGILA.Commons.DTOs.Vigencia
{
    public record VigenciaShortInfoDTO
    {
        public int VigenciaID { get; set; }
        public string NombreVigencia { get; set; } = String.Empty;
    }
}
