using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities
{
    [Table("DETALLE_UBICACION")]
    public class DetalleUbicacion
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("FK_TIPO_UBICACION")]
        [ForeignKey("FkTipoUbicacionNavigation")]
        public int FkTipoUbicacion { get; set; }
        [Column("DETALLE")]
        [MaxLength(100)]
        public string Detalle { get; set; }
        [Column("ESTADO")]
        public bool Estado { get; set; }
        [Column("RESPONSABLE_CAMBIO")]
        public Guid ResponsableID { get; set; }

        public virtual TipoUbicacion FkTipoUbicacionNavigation { get; set; }


    }
}
