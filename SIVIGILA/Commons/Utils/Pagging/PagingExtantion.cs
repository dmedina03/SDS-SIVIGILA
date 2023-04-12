using Microsoft.EntityFrameworkCore;

namespace SIVIGILA.Commons.Utils.Pagging
{
    public static class PagingExtantion
    {
        /// <summary>
        /// Método para páginar la consulta 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page">Página que se desea consultar</param>
        /// <param name="take">Cantidad de elementos a tomar</param>
        /// <returns>Colleccion de elementos presentes en la página actual</returns>
        public static async Task<DataCollection<T>> GetPagedAsync<T>(
            this IQueryable<T> query, int page, int take = 20)
        {
            var originalpage = page;
            page--;
            if (page > 0)
            {
                page = page * take;
            }

            var result = new DataCollection<T>()
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalpage,
            };

            result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            return result;
        }
        /// <summary>
        /// Método para obtener el conjunto de elementos resulaltes tras saltar {skip} elementos
        /// y tomar {take} elementos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skip">Cantidad de elementos a saltar</param>
        /// <param name="take">Cantidad de elementos a tomar</param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ToListBySkipAsync<T>(
            this IQueryable<T> query,int skip, int take)
        {
            int _take = take > 0 ? take : 20;
            int _skip = skip > 0 ? skip : 0;
            return await query.Skip(_skip).Take(_take).ToListAsync();
        }

    }
}
