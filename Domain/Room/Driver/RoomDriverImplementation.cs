namespace Domain.Room.Driver
{
    public class RoomDriverImplementation
    {
        public async Task<IReadOnlyList<RoomEntity>> FindAsync(ISpecification<RoomEntity> specification, CancellationToken cancellation = default)
        {
            if (specification is Specification<RoomEntity> spec)
            {
                return await spec.InvokeOnRepository(cancellation);
            }
            throw new ArgumentException("Unsupported specification type", nameof(specification));
        }
    }
}
