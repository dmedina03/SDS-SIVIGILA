using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("PRODUCTO_VIGENCIA")]
public partial class ProductosVigencia
{
    [Column("ID")]
    [Key]
    public int Id { get; set; }
    [Column("NOMBRE_PRODUCTO")]
    [MaxLength(100)]
    public string NombreProducto { get; set; } = string.Empty;
    [Column("UNIDAD_MEDIDA")]
    [MaxLength(100)]
    public string UnidadMedida { get; set; } = string.Empty;
    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid Responsable { get; set; }
}
