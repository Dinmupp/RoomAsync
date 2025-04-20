namespace Domain
{
    /// <summary>
    /// Non generic interface supporting <see cref="IDataEntityExposer{TIDataEntity}"/>.
    /// </summary>
    public interface IDataEntityExposer
    {
    }

    /// <summary>
    /// Interface is ONLY intended for enabling access to the underlying data model encapsulated by a Domain Entity.
    /// Concrete infrastructure implementation of Driven Port also known as a Recipient or Repository implementation, are the ONLY ones that should use this.
    /// </summary>
    /// <typeparam name="TIDataEntity">Represents the interface of the data structure required by the Domain Entity</typeparam>
    public interface IDataEntityExposer<TIDataEntity> : IDataEntityExposer
    {
        /// <summary>
        /// Exposes the underlying data model encapsulated by a Domain Entity.
        /// </summary>
        /// <typeparam name="TDataEntity">Represents a concrete implementation of <typeparamref name="TIDataEntity"/> provided by an adapter, e.g. an infrastructure implementation</typeparam>
        /// <returns></returns>
        public TDataEntity GetInstanceAs<TDataEntity>() where TDataEntity : class, TIDataEntity;
    }
}
