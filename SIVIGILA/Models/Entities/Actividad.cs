using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;

[Table("ACTIVIDAD")]
public  class Actividad
{
    [Key]
    public int Id { get; set; }

    [Column("NOMBRE_ACTIVIDAD")]
    [MaxLength(100)]
    public string NombreActividad { get; set; }
    [Column("DETALLE_ACTIVIDAD")]
    [MaxLength(150)]
    public string DetalleActividad { get; set; }
    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("FK_META")]
    [ForeignKey("FkMetaNavigation")]
    public int FkMeta { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }

    public virtual Meta FkMetaNavigation { get; set; }
}
