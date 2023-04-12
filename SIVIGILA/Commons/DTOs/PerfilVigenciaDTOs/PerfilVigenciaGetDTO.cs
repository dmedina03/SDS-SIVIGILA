namespace SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs
{
    public class PerfilVigenciaGetDTO
    {
        public int PerfilVigenciaID { get; set; }
        public int PerfilID { get; set; }
        public int VigenciaID { get; set; }
        public IEnumerable<PerfilProfesionVigenciaGetDto>? ProfesionVigencia { get; set; }
    }

    public class PerfilProfesionVigenciaGetDto
    {
        public int ProfesionVigenciaID { get; set; }
        public int ProfesionID { get; set; }
        public int PerfilVigenciaID { get; set; }
        public bool ListPostgrado { get; set; }
        public IEnumerable<PerfilPostgradoVigenciaGetDto>? PostgradoVigencia { get; set; }
    }

    public class PerfilPostgradoVigenciaGetDto
    {
        public int PostgradoVigenciaID { get; set; }
        public int PostgradoID { get; set; }
        public int ProfesionVigenciaID { get; set; }
    }



}
