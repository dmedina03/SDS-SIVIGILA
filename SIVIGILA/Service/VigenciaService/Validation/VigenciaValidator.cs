using FluentValidation;
using SIVIGILA.Commons.DTOs.Vigencia;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.VigenciaService.Validation
{
    public class VigenciaValidator : AbstractValidator<VigenciaDTO>
    {
        private readonly IVIgenciaRepository _repository;

        public VigenciaValidator(IVIgenciaRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.VigenciaID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });

            RuleSet("Any", () =>
            {

                RuleFor(x => x.VigenciaID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.VigenciaID == id))
                    .WithMessage("La vigencia asociada al ID no existe")
                    .When(x => x.VigenciaID != 0);
            });
        }
    }
}
