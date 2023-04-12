using FluentValidation;
using SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.DocumentoContServices.Validation
{
    public class DocumentoContValidator: AbstractValidator<DocumentoCDTO>
    {
        private readonly IDocumentosContRepository _repository;

        public DocumentoContValidator(IDocumentosContRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.DocumentoConID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.DocumentoConID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("El documento de contratación asociada al ID no existe")
                    .When(x => x.DocumentoConID != 0);

                RuleFor(x => x.NombreDocumento)
                .NotEmpty()
                .WithMessage("El nombre del documento no puede estar vacio")
                .MaximumLength(100)
                .WithMessage("El nombre del documento no debe tener mas de 100 caracteres")
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombreDocumento.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.DocumentoConID))
               .WithMessage("Ya existe un registro con el mismo nombre de documento");
            });
        }
    }
}
