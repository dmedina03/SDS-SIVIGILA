using FluentValidation;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.LineaService.Validation
{
    public class LineaValidator: AbstractValidator<LineaDTO>
    {
        private readonly ILineaRepository _repository;

        public LineaValidator(ILineaRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.LineaID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });

                
            RuleSet("Any",()=>
            {

                RuleFor(x => x.LineaID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("La linea asociada al ID no existe")
                    .When(x => x.LineaID != 0);

                RuleFor(x => x.NombreLinea)
                .NotEmpty()
                .WithMessage("El nombre de la linea no puede estar vacio")
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombreLinea.ToLower().Trim() == nombre.ToLower().Trim() && x.Id!=c.LineaID))
               .WithMessage("Ya existe un registro con el mismo nombre de linea");
            });
           
        }
    }
}
