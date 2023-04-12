using FluentValidation;
using SIVIGILA.Commons.DTOs.Profesion;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.ProfesionService.Validation
{
    public class ProfesionValidator : AbstractValidator<ProfesionDTO>
    {
        private readonly IProfesionRepository _repository;

        public ProfesionValidator(IProfesionRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.ProfesionID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.ProfesionID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("La Profesion asociada al ID no existe")
                    .When(x => x.ProfesionID != 0);

                RuleFor(x => x.NombreProfesion)
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombreProfesion.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.ProfesionID))
               .WithMessage("Ya existe un registro con el mismo nombre de Profesion");
            });

        }
    }
}
