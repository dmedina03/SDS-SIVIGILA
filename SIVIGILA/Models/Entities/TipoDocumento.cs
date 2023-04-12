using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("TIPO_DOCUMENTO")]
public partial class TipoDocumento
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("NOMBRE_DOCUMENTO")]
    [MaxLength(100)]
    public string NombreDocumento { get; set; }
    [Column("TALENTO_HUMANO")]
    public bool TalentoHumano { get; set; }
    [Column("IVC")]
    public bool Ivc { get; set; }
    [Column("SISPIC")]
    public bool Sispic { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }
}
