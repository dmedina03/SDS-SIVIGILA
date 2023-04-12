using FluentValidation;
using SIVIGILA.Commons.DTOs.Perfil;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.PerfilService.Validation
{
    public class PerfilValidator : AbstractValidator<PerfilDTO>
    {
        private readonly IPerfilRepository _repository;

        public PerfilValidator(IPerfilRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.PerfilID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.PerfilID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("La Perfil asociada al ID no existe")
                    .When(x => x.PerfilID != 0);

                RuleFor(x => x.NombrePerfil)
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombrePerfil.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.PerfilID))
               .WithMessage("Ya existe un registro con el mismo nombre de Perfil");
            });

        }
    }
}
