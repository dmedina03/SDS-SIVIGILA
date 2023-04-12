using FluentValidation;
using SIVIGILA.Commons.TipoDatoDTO;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.TipoDatoService.Validation
{
    public class TipoDatoValidator : AbstractValidator<TipoDatoDTO>
    {

        private readonly ITipoDatoRepository _tipoDatoRepository;
        public TipoDatoValidator(ITipoDatoRepository tipoDatoRepository)
        {
            _tipoDatoRepository = tipoDatoRepository;
            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id)
                .NotEqual(0)
                .WithMessage("Al actualizar un registro el ID no puede ser 0");
            });
            
            RuleSet("DescripcionEqual", () =>
            {
                RuleFor(x => x.Descripcion)
                .MustAsync(async (x,descripcion,ct ) => 
                    !await _tipoDatoRepository.ExistGenericAsync(x => x.Descripcion.ToLower().Trim()==descripcion.ToLower().Trim()) )
                .WithMessage("No puede existir un tipo de dato con una descripción igual.");
            });
        }
    }
}
