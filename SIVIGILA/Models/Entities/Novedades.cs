using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("NOVEDADES")]
public partial class Novedades
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NOMBRE_NOVEDAD")]
    public string NombreNovedad { get; set; }
    [Column("Estado")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }
}
