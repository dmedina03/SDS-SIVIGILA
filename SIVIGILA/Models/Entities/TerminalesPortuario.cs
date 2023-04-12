using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table ("TERMINALES_PORTUARIOS")]
public partial class TerminalesPortuario
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NOMBRE_TERMINAL")]
    [MaxLength(100)]
    public string NombreTerminalesPortuario { get; set; }

    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }
}
