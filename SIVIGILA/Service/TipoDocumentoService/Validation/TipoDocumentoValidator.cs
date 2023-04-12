using FluentValidation;
using SIVIGILA.Commons.DTOs.TipoDocumento;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.TipoDocumentoService.Validation
{
    public class TipoDocumentoValidator: AbstractValidator<TipoDocumentoDTO>
    {
        private readonly ITipoDocumentoRepository _repository;
        public TipoDocumentoValidator(ITipoDocumentoRepository repository)
        {
            _repository = repository;
            RuleSet("Create", () =>
            {
                RuleFor(x => x.TipoDocumentoID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.TipoDocumentoID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("El tipo documento asociada al ID no existe")
                    .When(x => x.TipoDocumentoID != 0);

                RuleFor(x => x.NombreDocumento)
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombreDocumento.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.TipoDocumentoID))
               .WithMessage("Ya existe un registro con el mismo nombre de tipo documento");
            });
        }
    }
}
