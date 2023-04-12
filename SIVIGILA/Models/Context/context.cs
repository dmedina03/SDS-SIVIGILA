using Microsoft.EntityFrameworkCore;
using SIVIGILA.Models.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection.Metadata;

namespace SIVIGILA.Models.Context
{
    public class context: DbContext
    {
        public context()
        {
            

        }
        public context(DbContextOptions<context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Linea>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_LINEA"));
            modelBuilder.Entity<Meta>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_META"));
            modelBuilder.Entity<Actividad>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_ACTIVIDAD"));
            modelBuilder.Entity<ProductosVigencia>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_PRODUCTO_VIGNCIA"));
            modelBuilder.Entity<DocumentacionContratacion>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_DOCUMENTACION_CONTRATACION"));
            modelBuilder.Entity<Novedades>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_NOVEDADES"));
            modelBuilder.Entity<DetalleUbicacion>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_DETALLE_UBICACION"));
            modelBuilder.Entity<TipoUbicacion>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_TIPO_UBICACION"));
            modelBuilder.Entity<PostgradoVigencia>()
               .ToTable(tb => tb.HasTrigger("TRASABILIDAD_POSTGRADO_VIGENCIA"));
            modelBuilder.Entity<ProfesionVigencia>()
               .ToTable(tb => tb.HasTrigger("TRASABILIDAD_PROFESION_VIGENCIA"));
            modelBuilder.Entity<PerfilVigencia>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_PERFIL_VIGENCIA"));
            modelBuilder.Entity<Postgrado>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_POSTGRADO"));
            modelBuilder.Entity<TerminalesPortuario>()
               .ToTable(tb => tb.HasTrigger("TRASABILIDAD_TERMINALES_PORTUARIOS"));
            modelBuilder.Entity<TipoDocumento>()
               .ToTable(tb => tb.HasTrigger("TRASABILIDAD_POSTGRADO"));
            modelBuilder.Entity<Profesion>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_PROFESION"));
            modelBuilder.Entity<Perfil>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_PERFIL"));
            modelBuilder.Entity<DpSexo>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_DP_SEXO"));
             modelBuilder.Entity<DpCondiDiscapa>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_DP_CONDI_DISCAPA"));
             modelBuilder.Entity<DpOrientSexual>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_DP_ORIENT_SEXUAL"));
             modelBuilder.Entity<DpPresenEtnica>()
                .ToTable(tb => tb.HasTrigger("TRASABILIDAD_DP_PRESEN_ETNICA"));

        }

        public virtual DbSet<Vigencia> Vigencias { get; set; }
        public virtual DbSet<TipoUbicacion> TipoUbicacion { get; set; }
        public virtual DbSet<DetalleUbicacion> DetalleUbicacion { get; set; }
        public virtual DbSet<TipoDato> TipoDato { get; set; }
        public virtual DbSet<ProductosVigencia> ProductosVigencias { get; set; }
        public virtual DbSet<Linea> Lineas { get; set; }
        public virtual DbSet<DocumentacionContratacion> DocumentosContratacion { get; set; }
        public virtual DbSet<Novedades> Novedades { get; set; }

        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Postgrado> Postgrados { get; set; }
        public virtual DbSet<Profesion> Profesions { get; set; }
        public virtual DbSet<TerminalesPortuario> TerminalesPortuarios { get; set; }

    }
}
