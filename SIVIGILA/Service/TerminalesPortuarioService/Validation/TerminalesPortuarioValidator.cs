using FluentValidation;
using SIVIGILA.Commons.DTOs.TerminalesPortuario;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.TerminalesPortuarioService.Validation
{
    public class TerminalesPortuarioValidator : AbstractValidator<TerminalesPortuarioDTO>
    {
        private readonly ITerminalesPortuarioRepository _repository;

        public TerminalesPortuarioValidator(ITerminalesPortuarioRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.TerminalesPortuarioID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.TerminalesPortuarioID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("La TerminalesPortuario asociada al ID no existe")
                    .When(x => x.TerminalesPortuarioID != 0);

                RuleFor(x => x.NombreTerminalesPortuario)
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombreTerminalesPortuario.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.TerminalesPortuarioID))
               .WithMessage("Ya existe un registro con el mismo nombre de TerminalesPortuario");
            });

        }
    }
}
