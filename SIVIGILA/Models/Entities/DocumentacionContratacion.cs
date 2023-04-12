using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;

[Table("DOCUMENTACION_CONTRATACION")]
public partial class DocumentacionContratacion
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("NOMBRE_DOCUMENTO")]
    public string NombreDocumento { get; set; }
    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }
}
