using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SIVIGILA.Commons.ErrorHandling.Middleware;
using SIVIGILA.Models.Context;
using SIVIGILA.Repository;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.VigenciaService;
using SIVIGILA.Commons.ValidationAndResult;
using SIVIGILA.Service.LineaService;
using FluentValidation;
using SIVIGILA.Service.TipoUbicacionService;
using SIVIGILA.Service.MetaService;
using SIVIGILA.Service.ActividadService;
using SIVIGILA.Service.ProductosVigenciaServices;
using SIVIGILA.Service.DocumentoContServices;
using SIVIGILA.Service.NovedadesService;
using SIVIGILA.Service.PerfilService;
using SIVIGILA.Service.PostgradoService;
using SIVIGILA.Service.ProfesionService;
using SIVIGILA.Service.TerminalesPortuarioService;
using SIVIGILA.Service.TipoDocumentoService;
using SIVIGILA.Service.PerfilVigenciaService;
using SIVIGILA.Service.ProfesionVigenciaService;
using SIVIGILA.Service.PostgradoVigenciaService;
using SIVIGILA.Service.TipoDatoService;
using SIVIGILA.Service.DetalleUbicacionService;
using SIVIGILA.Service.DpSexoService;
using SIVIGILA.Service.DpPresenEtnicaServices;
using SIVIGILA.Service.DpCondiDiscapaService;
using SIVIGILA.Service.DpOrientSexualService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<context>(options => options.
    UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomValidationFilterAttributecs>();
})
                                .ConfigureApiBehaviorOptions(options =>
                                {
                                    options.SuppressModelStateInvalidFilter = true;
                                });



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "default",
                      policy =>
                      {
                          policy.WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
                                    .WithHeaders(
                                    HeaderNames.Accept,
                                    HeaderNames.ContentType,
                                    HeaderNames.Authorization)
                                    .AllowCredentials()
                                    .SetIsOriginAllowed(origin =>
                                    {
                                        if (string.IsNullOrWhiteSpace(origin)) return true;

                                        return true;
                                    });
                      });
});

//Validator
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

//Actividad GroupService
builder.Services.AddScoped<IActividadRepository, ActividadRepository>();
builder.Services.AddScoped<IActividadService, ActividadService>();

//VigenciaGroupService
builder.Services.AddScoped<IVIgenciaRepository, VigenciaRepository > ();
builder.Services.AddScoped<IVigenciaService, VigenciaService > ();
//TipoUbicacion GroupService
builder.Services.AddScoped<ITipoUbicacionRepository, TipoUbicacionRepository>();
builder.Services.AddScoped<ITipoUbicacionService, TipoUbicacionService>();

//TipoDato GroupService
builder.Services.AddScoped<ITipoDatoRepository, TipoDatoRepository>();
builder.Services.AddScoped<ITipoDatoService, TipoDatoService>();

//DetalleUbicacion GroupService
builder.Services.AddScoped<IDetalleUbicacionRepository, DetalleUbicacionRepository>();
builder.Services.AddScoped<IDetalleUbicacionService, DetalleUbicacionService>();

//ProductosVigencia GroupService
builder.Services.AddScoped<IProductosVigenciaRepository, ProductosVigenciaRepository>();
builder.Services.AddScoped<IProductoVigenciaService, ProductosVigenciaService>();


//Estado GroupService
builder.Services.AddScoped<IEstadoRepository, EstadoRepository> ();

//Linea GropuService
builder.Services.AddScoped<ILineaRepository, LineaRepository>();
builder.Services.AddScoped<ILineaService, LineaService>();

//Meta GroupService
builder.Services.AddScoped<IMetaRepository, MetaRepository>();
builder.Services.AddScoped<IMetaService, MetaService>();

//DocumentoContratacion GroupService
builder.Services.AddScoped<IDocumentosContRepository, DocumentoContRepository> ();
builder.Services.AddScoped<IDocumentoContService, DocumentoContService>();

//Novedades GroupService
builder.Services.AddScoped<INovedadRepository, NovedadRepository>();
builder.Services.AddScoped<INovedadService, NovedadService>();
//TerminalesPortuario GropuService
builder.Services.AddScoped<ITerminalesPortuarioRepository, TerminalesPortuarioRepository>();
builder.Services.AddScoped<ITerminalesPortuarioService, TerminalesPortuarioService>();

//TipoDocumento GropuService
builder.Services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();
builder.Services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();


//Perfil GropuService
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();
builder.Services.AddScoped<IPerfilService, PerfilService>();

//Postgrado GropuService
builder.Services.AddScoped<IPostgradoRepository, PostgradoRepository>();
builder.Services.AddScoped<IPostgradoService, PostgradoService>();

//Profesion GropuService
builder.Services.AddScoped<IProfesionRepository, ProfesionRepository>();
builder.Services.AddScoped<IProfesionService, ProfesionService>();

//PerfilVigencia GroupService
builder.Services.AddScoped<IPerfilVigenciaRepository, PerfilVigenciaRepository>();
builder.Services.AddScoped<IPerfilVigenciaService, PerfilVigenciaService>();

//ProfesionVigencia GroupService
builder.Services.AddScoped<IProfesionVigenciaRepository, ProfesionVigenciaRepository>();
builder.Services.AddScoped<IProfesionVigenciaService, ProfesionVigenciaService>();

//PostgradoVigencia GroupService
builder.Services.AddScoped<IPostgradoVigenciaRepository, PostgradoVigenciaRepository>();
builder.Services.AddScoped<IPostgradoVigenciaService, PostgradoVigenciaService>();
//DpSexo GroupService
builder.Services.AddScoped<IDpSexoRepository, DpSexoRepository>();
builder.Services.AddScoped<IDpSexoService, DpSexoService>();
//DpPresenEtnica GroupService
builder.Services.AddScoped<IDpPresenEtnicaRepository, DpPresenEtnicaRepository>();
builder.Services.AddScoped<IDpPresenEtnicaService, DpPresenEtnicaService>();
//DpCondiDiscapa GroupService
builder.Services.AddScoped<IDpCondiDiscapaRepository, DpCondiDiscapaRepository>();
builder.Services.AddScoped<IDpCondiDiscapaService, DpCondiDiscapaService>();
//DpCondiDiscapa GroupService
builder.Services.AddScoped<IDpOrientSexualRepository, DpOrientSexualRepository>();
builder.Services.AddScoped<IDpOrientSexualService, DpOrientSexualService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
