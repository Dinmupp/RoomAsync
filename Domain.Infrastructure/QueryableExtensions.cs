using System.Linq.Expressions;

namespace Domain.Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ConditionalWhere<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }


        public static IQueryable<T> ApplySortingAndPaging<T>(
                this IQueryable<T> source,
                string? sortBy,
                bool sortAscending,
                Range? offset)
        {
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.PropertyOrField(parameter, sortBy);
                var converted = Expression.Convert(property, typeof(object));
                var sortExpression = Expression.Lambda<Func<T, object>>(converted, parameter);

                source = sortAscending
                    ? source.OrderBy(sortExpression)
                    : source.OrderByDescending(sortExpression);
            }

            if (offset?.End.Value > 0)
            {
                source = source
                    .Skip(offset.Value.Start.Value)
                    .Take(offset.Value.End.Value);
            }

            return source;
        }


    }
}
