using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities
{
    [Table("ESTADO")]
    public class Estado
    {
        [Key]
        [Column("ESTADO_ID")]
        public int Estado_ID { get; set; }
        [Column("TIPO")]
        public string Tipo { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        public virtual ICollection<Vigencia> Vigencias { get; }
    }
}
