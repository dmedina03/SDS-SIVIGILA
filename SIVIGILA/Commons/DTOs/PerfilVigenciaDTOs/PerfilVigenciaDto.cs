using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs
{
    public class PerfilVigenciaDto
    {
        public int PerfilVigenciaID { get; set; }
        public int PerfilID { get; set; }
        public int VigenciaID { get; set; }
        public List<PerfilProfesionVigenciaDto>? ProfesionVigencia { get; set; }

        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }

    public class PerfilProfesionVigenciaDto
    {
        public int ProfesionVigenciaID { get; set; }
        public int ProfesionID { get; set; }
        public int PerfilVigenciaID { get; set; }
        public bool ListPostgrado { get; set; }
        public List<PerfilPostgradoVigenciaDto>? PostgradoVigencia { get; set; }

        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }

    public class PerfilPostgradoVigenciaDto
    {
        public int PostgradoVigenciaID { get; set; }
        public int PostgradoID { get; set; }
        public int ProfesionVigenciaID { get; set; }

        [JsonIgnore]
        public Guid ResponsableID { get; set; }
    }



}
