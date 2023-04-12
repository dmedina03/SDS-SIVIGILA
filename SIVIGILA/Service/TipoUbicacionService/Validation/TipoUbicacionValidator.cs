using FluentValidation;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.Enums;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.TipoUbicacionService.Validation
{
    public class TipoUbicacionValidator : AbstractValidator<TipoUbicacionDto>
    {

        private readonly ITipoUbicacionRepository _tipoUbicacionRepository;
        private readonly IDetalleUbicacionRepository _detalleUbicacionRepository;
        public TipoUbicacionValidator(ITipoUbicacionRepository tipoUbicacionRepository, IDetalleUbicacionRepository detalleUbicacionRepository)
        {
            _tipoUbicacionRepository = tipoUbicacionRepository;
            _detalleUbicacionRepository = detalleUbicacionRepository;

            RuleSet("Any", () =>
            {
                RuleFor(x => x.Ubicacion).
                MustAsync(async (c, nombre, ct) => !await _tipoUbicacionRepository.
                ExistGenericAsync(x => x.Ubicacion.ToLower().Trim() == nombre.ToLower().Trim() && x.Id != c.Id)).
                WithMessage("Ya existe un Tipo de Ubicacion con el mismo nombre");

                RuleFor(x => x.FkTipoDato)
                .MustAsync(async (c, tipoDato, ct) => await ValidateTipoDato(tipoDato, c.Id, c.ValoresListaUbicacion))
                .WithMessage("El tipo de dato 'Valor único' solamente puede tener un valor de la lista");

            });
            RuleSet("Create", () =>
            {
                RuleFor(x => x.Id)
                .Equal(0)
                .WithMessage("Para crear el ID debe ser igual a 0");

                RuleFor(x => x.FkTipoDato)
                .MustAsync(async (c, tipoDato, ct) => !(tipoDato == (int)TipoDatoEnums.ValorUnico && c.ValoresListaUbicacion.Count > 1))
                .WithMessage("Al crear un Tipo de Ubicacion con un Tipo de dato 'Valor único' no se puede agregar mas de un valor a la lista");

            });
            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id)
                .NotEqual(0)
                .WithMessage("Para actualizar un registro el ID no debe ser igual a 0");
            });

        }
        
        private async Task<bool> ValidateTipoDato(int tipoDato, int tipoUbi, List<DetalleUbicacionDto> valoresLista)
        {
            
            if (tipoDato == (int)TipoDatoEnums.ValorUnico)
            {
                var cantidadEntidad = await _detalleUbicacionRepository.GenericGetAllAsync(x => x.FkTipoUbicacion == tipoUbi);

                if (cantidadEntidad.Count > 1 || valoresLista.Count > 1)
                {
                    return false;
                }
                else if (cantidadEntidad.Count < 1 && valoresLista.Count <= 1)
                {
                    return true;
                }
                else
                {
                    if (cantidadEntidad.Count == 1 && valoresLista.Count == 1 
                        && (cantidadEntidad.FirstOrDefault()).Id != (valoresLista.FirstOrDefault()).Id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
