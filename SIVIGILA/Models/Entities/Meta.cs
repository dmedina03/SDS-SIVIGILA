using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;

[Table("METAS")]
public partial class Meta
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("NOMBRE_META")]
    [MaxLength(100)]
    public string NombreMeta { get; set; }
    [Column("DETALLE_META")]
    [MaxLength(150)]
    public string DetalleMeta { get; set; }
    [Column("FK_VIGENCIA")]
    [ForeignKey("FkVigenciaNavigation")]
    public int FkVigencia { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }

    public virtual ICollection<Actividad> Actividads { get; set; } = new List<Actividad>();

    public virtual Vigencia FkVigenciaNavigation { get; set; }
}
