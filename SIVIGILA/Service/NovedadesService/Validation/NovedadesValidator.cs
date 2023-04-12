using FluentValidation;
using SIVIGILA.Commons.DTOs.NovedadesDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.NovedadesService.Validation
{
    public class NovedadesValidator : AbstractValidator<NovedadesDTO>
    {
        private readonly INovedadRepository _repository;
        public NovedadesValidator(INovedadRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.NovedadesID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.NovedadesID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("La Novedad asociada al ID no existe")
                    .When(x => x.NovedadesID != 0);

                RuleFor(x => x.NombreNovedad)
                .NotEmpty()
                .WithMessage("El nombre de la novedad no puede estar vacio")
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombreNovedad.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.NovedadesID))
               .WithMessage("Ya existe un registro con el mismo nombre de linea");
            });

        }
    }
}
