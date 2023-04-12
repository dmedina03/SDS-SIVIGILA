using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Models.Entities;
[Table("DP_CONDI_DISCAPA")]
public partial class DpCondiDiscapa
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("DESCRIPCION")]
    [MaxLength(100)]
    public string? Descripcion { get; set; }
    [Column("TALENTO_HUMANO")]
    public bool TalentoHumano { get; set; }
    [Column("IVC")]
    public bool Ivc { get; set; }
    [Column("VSA")]
    public bool Vsa { get; set; }
    [Column("RESPONSABLE_CAMBIO")]
    public Guid ResponsableID { get; set; }
}
