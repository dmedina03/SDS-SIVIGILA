using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities
{
    [Table("TABLA_COSTOS")]
    public class TablaCostos
    {
        [Column("ID")]
        [Key]
        public int ID { get; set; }
        [Column("PERFIL_VIGENCIA_ID")]
        [ForeignKey("PerfilVigencia")]
        public int PerfilVigencia_ID { get; set; }
        [Column("PCT_TALENTO_HUMANO")]
        public int Pct_TalentoHumano { get; set; }
        [Column("PCT_INSUMOS")]
        public int Pct_Insumos { get; set; }
        [Column("PCT_ADMINISTRACION")]
        public int Pct_Administracion { get; set; }
        [Column("PCT_RURALIDAD")]
        public int Pct_Ruralidad { get; set; }
        [Column("HORA_MES")]
        public int HoraMes { get; set; }
        [Column("VALOR_TALENTO_HUMANO")]
        public decimal Valor_TalentoHumano { get; set; }
        [Column("VALOR_INSUMOS")]
        public decimal Valor_Insumos { get; set; }
        [Column("VALOR_ADMINISTRACION")]
        public decimal Valor_Administracion { get; set; }
        [Column("VALOR_MES_HORA_URBANO")]
        public decimal Valor_MesHoraUrbano { get; set; }
        [Column("VALOR_HORA_URBANO")]
        public decimal Valor_HoraUrbano { get; set; }
        [Column("VALOR_MES_HORA_RURAL")]
        public decimal Valor_MesHoraRural { get; set; }
        [Column("VALOR_HORA_RURAL")]
        public decimal Valor_HoraRural { get; set; }
        [Column("VALOR_TALENTO_HUMANO_HORA_URBANA")]
        public decimal Valor_TalentoHumanoHoraUrbano { get; set; }
        [Column("VALOR_TALENTO_HUMANO_HORA_RURAL")]
        public decimal Valor_TalentoHumanoHoraRural { get; set; }
        public virtual PerfilVigencia? PerfilVigencia { get; set; }
    }
}
