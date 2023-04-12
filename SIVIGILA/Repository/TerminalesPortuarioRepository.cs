using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TerminalesPortuario;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class TerminalesPortuarioRepository : BaseRepository<TerminalesPortuario>, ITerminalesPortuarioRepository
    {
        public TerminalesPortuarioRepository(context context) : base(context)
        {
        }

        public async Task<DataCollection<TerminalesPortuarioDTO>> GetByParamsAsync(SearchTerminalesPortuarioDTO parameters)
        {
            var data = Entity.Select(x => new TerminalesPortuarioDTO
            {
                TerminalesPortuarioID = x.Id,
                NombreTerminalesPortuario = x.NombreTerminalesPortuario,
                Estado = x.Estado,
            });
            if (!String.IsNullOrEmpty(parameters.NombreTerminalesPortuario))
            {
                data = data.Where(x => x.NombreTerminalesPortuario.ToLower()
                            .Trim().Contains(parameters.NombreTerminalesPortuario.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);

        }
    }
}
