using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("POSTGRADO")]
public partial class Postgrado
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("NOMBRE_POSTGRADO")]
    [MaxLength(100)]
    public string NombrePostgrado { get; set; }
    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid ResponsableID { get; set; }
    public virtual ICollection<PostgradoVigencia> PostgradoVigencias { get; set; }
}
