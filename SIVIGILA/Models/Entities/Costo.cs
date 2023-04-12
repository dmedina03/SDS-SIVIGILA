using System;
using System.Collections.Generic;

namespace SIVIGILA.Models.Entities;


public partial class Costo
{
    public int Id { get; set; }

    public double? ValorBaseTalentoHumano { get; set; }

    public double? ValorInsumos { get; set; }

    public double? ValorAdministracion { get; set; }

    public double? ValorMesHoraUrbano { get; set; }

    public double? ValorHoraUrbano { get; set; }

    public double? ValorMesHoraRural { get; set; }

    public double? ValorHoraRural { get; set; }

    public double? ValorBaseTalentoHumanoHoraUrbano { get; set; }

    public double? ValorBaseTalentoHumanoHoraRural { get; set; }

    public virtual ICollection<CostoVigencium> CostoVigencia { get; } = new List<CostoVigencium>();
}
