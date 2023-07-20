using Aqar.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Infrastructure.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable.Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage);
        }
    }
}