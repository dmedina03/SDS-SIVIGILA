using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities
{
    [Table("POSTGRADO_VIGENCIA")]
    public class PostgradoVigencia
    {
        [Key]
        [Column("POSTGRADO_VIGENCIA_ID")]
        public int PostgradoVigenciaID { get; set; }
        [Column("POSTGRADO_ID")]
        [ForeignKey("Postgrado")]
        public int PostgradoID { get; set; }
        [Column("PROFESION_VIGENCIA_ID")]
        [ForeignKey("ProfesionVigencia")]
        public int ProfesionVigenciaID { get; set; }

        [Column("RESPONSABLE_CAMBIO")]
        public Guid ResponsableCambio { get; set; }
        public virtual Postgrado Postgrado { get; set; }
        public virtual ProfesionVigencia ProfesionVigencia { get; set; }
    }
}
