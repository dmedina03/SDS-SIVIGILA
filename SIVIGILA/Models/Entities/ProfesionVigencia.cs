using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities
{
    [Table("PROFESION_VIGENCIA")]
    public class ProfesionVigencia
    {
        [Key]
        [Column("PROFESION_VIGENCIA_ID")]
        public int ProfesionVigenciaID { get; set; }
        [Column("PROFESION_ID")]
        [ForeignKey("Profesion")]
        public int ProfesionID { get; set; }
        [Column("PERFIL_VIGENCIA_ID")]
        [ForeignKey("PerfilVigencia")]
        public int PerfilVigenciaID { get; set; }

        [Column("RESPONSABLE_CAMBIO")]
        public Guid ResponsableCambio { get; set; }
        public virtual Profesion Profesion { get; set; }
        public virtual PerfilVigencia PerfilVigencia { get; set; }

        public virtual ICollection<PostgradoVigencia> Postgrados { get; set; }

    }


}
