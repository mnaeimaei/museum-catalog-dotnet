//IArtifact.cs

using System.Linq.Expressions;

namespace Museum.Collection.Catalog.Domain.Interfaces;

public interface IRepository<T>
{
    Task<T?> SingleAsync(Guid id);

    Task<IReadOnlyList<T>> WhereAsync(Expression<Func<T, bool>> predicate);

    Task<IReadOnlyList<T>> WhereAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
}
