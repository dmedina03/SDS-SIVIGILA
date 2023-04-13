using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class TablaCostosRepository : BaseRepository<TablaCostos>, ITablaCostosRepository
    {

        public TablaCostosRepository(context context) : base(context)
        {

        }


    }
}
