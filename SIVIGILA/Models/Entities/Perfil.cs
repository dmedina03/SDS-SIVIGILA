using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("PERFIL")]
public partial class Perfil
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("NOMBRE_PERFIL")]
    [MaxLength(100)]
    public string NombrePerfil { get; set; }
    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid ResponsableID { get; set; }

    public virtual ICollection<PerfilVigencia> PerfilesVigencias { get; set; }
}
