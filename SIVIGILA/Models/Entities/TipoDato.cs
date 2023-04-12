using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("TIPO_DATO")]
public class TipoDato
{
    [Column("ID")]
    [Key]
    public int Id { get; set; }
    [Column("DESCRIPCION")]
    public string Descripcion { get; set; }

    public virtual ICollection<TipoUbicacion> TipoUbicacions { get; } = new List<TipoUbicacion>();
}
