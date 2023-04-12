namespace SIVIGILA.Commons.DTOs.Vigencia
{
    public record VigenciaDTO: VigenciaSimpleDTO
    {
        public List<VigenciaSimpleDTO>? VigenciasAdicionales { get; set; }
    }
}
