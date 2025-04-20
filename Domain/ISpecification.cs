namespace Domain
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity); // Evaluate the criteria
    }
}
