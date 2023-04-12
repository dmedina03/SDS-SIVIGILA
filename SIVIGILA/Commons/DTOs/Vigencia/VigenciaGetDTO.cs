namespace SIVIGILA.Commons.DTOs.Vigencia
{
    public record VigenciaGetDTO: VigenciaSimpleGetDTO
    {
        public string Adiciones 
        {
            get
            {
                return (VigenciasAdicionales?.Count() ??0)!=0 ? "Si":"No";
            } 
        }
        public List<VigenciaSimpleGetDTO>? VigenciasAdicionales { get; set; }
    }
}
