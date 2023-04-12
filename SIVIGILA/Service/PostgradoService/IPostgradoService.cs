
using FluentValidation;
using SIVIGILA.Commons.DTOs.PostgradoDto;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.BaseInterfaces;
using SIVIGILA.Service.PostgradoService.Utils;

namespace SIVIGILA.Service.PostgradoService
{
    public interface IPostgradoService : ICreateOrUpdateRangeService<PostgradoDTO>, ICreateService<PostgradoDTO>,
                                    IUpdateService<PostgradoDTO>, IGetService<PostgradoDTO, PostgradoDTO>,
                                    IUpdateStateService, IExistService,
                                    IServiceSearch<PostgradoDTO, SearchPostgradoDTO>
    {
        public Task<bool> ExistByNameAsync(string nombrePostgrado);
    }
}