using FluentValidation;
using SIVIGILA.Commons.DTOs.PostgradoDto;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.PostgradoService.Validation
{
    public class PostgradoValidator : AbstractValidator<PostgradoDTO>
    {
        private readonly IPostgradoRepository _repository;

        public PostgradoValidator(IPostgradoRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.PostgradoID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.PostgradoID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("La Postgrado asociada al ID no existe")
                    .When(x => x.PostgradoID != 0);

                RuleFor(x => x.NombrePostgrado)
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombrePostgrado.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.PostgradoID))
               .WithMessage("Ya existe un registro con el mismo nombre de Postgrado");
            });

        }
    }
}
