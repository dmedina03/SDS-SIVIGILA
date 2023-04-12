using FluentValidation;
using SIVIGILA.Commons.DTOs.ProductosVigenciaDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.ProductosVigenciaServices.Validations
{
    public class ProductoVigenciaValidator: AbstractValidator<ProductosVigenciaDTO>
    {
        private readonly IProductosVigenciaRepository _repository;
        public ProductoVigenciaValidator(IProductosVigenciaRepository repository)
        {
            _repository = repository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.ProductoVigenciaID)
                   .Equal(0)
                   .WithMessage("Los IDs deben ser 0 en creación");
            });


            RuleSet("Any", () =>
            {

                RuleFor(x => x.ProductoVigenciaID)
                    .MustAsync(async (x, id, ct) => await _repository.ExistGenericAsync(x => x.Id == id))
                    .WithMessage("La linea asociada al ID no existe")
                    .When(x => x.ProductoVigenciaID != 0);

                RuleFor(x => x.NombreProducto)
                .NotEmpty()
                .WithMessage("El nombre del producto no puede estar vacio")
               .MustAsync(async (c, nombre, ct) => !await _repository.
               ExistGenericAsync(x => x.NombreProducto.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.ProductoVigenciaID))
               .WithMessage("Ya existe un registro con el mismo nombre de linea");
            });

        }
    }
}
