﻿using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IDpCondiDiscapaRepository : IBaseGenericRepository<DpCondiDiscapa>, 
                                                 ISearchRepository<DpCondiDiscapaDTO,SearchDpCondiDiscapaDTO>
    {
    }
}
