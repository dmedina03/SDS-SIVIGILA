using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("LINEAS")]
public partial class Linea
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("NOMBRE_LINEA")]
    [MaxLength(100)]

    public string NombreLinea { get; set; }
    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }
}
