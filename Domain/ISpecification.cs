namespace Domain
{
    public interface ISpecification<T>
    {
        Task<(IReadOnlyList<T> items, int totalCount)> InvokeOnRepository(CancellationToken cancellation = default);
        bool IsSatisfiedBy(T entity); // Evaluate the criteria
    }
}
