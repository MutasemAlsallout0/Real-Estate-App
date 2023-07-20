using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Infrastructure.Extensions
{
    public static class HttpContextExtension
    {
        public async static Task InsertParametersPaginationHeaders<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            // select count(*) from publihser
            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmountOfRecords", count.ToString());
        }
    }
}