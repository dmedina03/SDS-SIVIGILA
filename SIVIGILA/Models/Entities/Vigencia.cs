using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities
{
    [Table("VIGENCIA")]
    public class Vigencia
    {
        [Key]
        [Column("VIGENCIA_ID")]
        public int VigenciaID { get; set; }
        [Column("PRESUPUESTO")]
        public string Presupuesto { get; set; }
        [Column("FECHA_INICIO")]
        public DateTime FechaInicio { get; set; }
        [Column("FECHA_FIN")]
        public DateTime FechaFin { get; set; }
        [Column("ESTADO_VIGENCIA_ID")]
        [ForeignKey("EstadoVigencia")]
        public int Estado_Vigencia_ID { get; set; }
        [Column("ADICION_TIEMPO")]
        public DateTime? AdicionTiempo { get; set; }
        [Column("ESTADO")]
        public bool Estado { get; set; }
        [Column("FK_VIGENCIA_INICIAL")]
        [ForeignKey("VigenciaInicial")]
        public int? FK_Vigencia_Inicial { get; set; }

        public virtual Estado EstadoVigencia { get; set; }
        public virtual Vigencia? VigenciaInicial { get; set; }
        public virtual ICollection<Vigencia> VigenciasAdicionales { get; set; }

        public virtual ICollection<PerfilVigencia> Perfiles { get; set; }
        public virtual ICollection<Meta> Metas { get; set; }
        public virtual ICollection<CostoVigencium> CostosVigencia { get; set; }
    }
}
