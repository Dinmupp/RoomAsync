namespace Domain
{
    public interface ISpecification<T>
    {
        Task<IReadOnlyList<T>> InvokeOnRepository(CancellationToken cancellation = default);
        bool IsSatisfiedBy(T entity); // Evaluate the criteria
    }
}
