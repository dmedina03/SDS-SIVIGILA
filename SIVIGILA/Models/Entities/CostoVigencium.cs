using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;

[Table("COSTO_VIGENCIA")]
public partial class CostoVigencium
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("FK_VIGENCIA")]
    [ForeignKey("FkVigenciaNavigation")]
    public int? FkVigencia { get; set; }
    [Column("FK_COSTO")]
    [ForeignKey("FkCostoNavigation")]
    public int? FkCosto { get; set; }

    public virtual Costo? FkCostoNavigation { get; set; }

    public virtual Vigencia? FkVigenciaNavigation { get; set; }
}
