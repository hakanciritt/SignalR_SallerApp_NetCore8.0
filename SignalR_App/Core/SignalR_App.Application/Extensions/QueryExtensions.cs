using SignalR_App.Domain.Pageds;
using System.Linq.Expressions;

namespace SignalR_App.Application.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> PageBy<T>(IQueryable<T> query, IPagedRequest pagedRequest)
        {
            return query.Skip(pagedRequest.PageCount * pagedRequest.PageSize).Take(pagedRequest.PageSize);
        }
        public static IQueryable<T> PageBy<T>(IQueryable<T> query, int pageSize, int pageCount = 0)
        {
            return query.Skip(pageCount * pageSize).Take(pageSize);
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> expression)
        {
            return condition ? query.Where(expression) : query;
        }
    }
}
