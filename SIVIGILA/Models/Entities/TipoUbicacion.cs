using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;

[Table("TIPO_UBICACION")]
public class TipoUbicacion
{
    [Column("ID")]
    [Key]
    public int Id { get; set; }
    [Column("UBICACION")]
    [MaxLength(100)]
    public string Ubicacion { get; set; }
    [Column("FK_TIPO_DATO")]
    [ForeignKey("FkTipoDatoNavigation")]
    public int FkTipoDato { get; set; }
    [Column("ESTADO")]
    public bool Estado { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid ResponsableCambio { get; set; }
    public virtual TipoDato? FkTipoDatoNavigation { get; set; }
    public virtual ICollection<DetalleUbicacion> DetalleUbicacions { get; set; } = new List<DetalleUbicacion>();

}
