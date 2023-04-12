using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities
{
    [Table("PERFIL_VIGENCIA")]
    public class PerfilVigencia
    {
        [Key]
        [Column("PERFIL_VIGENCIA_ID")]
        public int PerfilVigenciaID { get; set; }
        [Column("PERFIL_ID")]
        [ForeignKey("Perfil")]
        public int PerfilID { get; set; }
        [Column("VIGENCIA_ID")]
        [ForeignKey("Vigencia")]
        public int VigenciaID { get; set; }

        [Column("RESPONSABLE_CAMBIO")]
        public Guid ResponsableCambio { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Vigencia Vigencia { get; set; }

        public virtual ICollection<ProfesionVigencia> Profesiones { get; set; }
    }
}
