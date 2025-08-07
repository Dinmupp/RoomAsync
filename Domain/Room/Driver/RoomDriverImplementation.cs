using Domain.Room.Response;

namespace Domain.Room.Driver
{
    public class RoomDriverImplementation : IRoomDriverPort
    {
        public async Task<GetAllResponse> FindAsync(ISpecification<RoomEntity> specification, CancellationToken cancellation = default)
        {
            try
            {
                var result = await specification.InvokeOnRepository(cancellation);
                return new GetAllResponse
                {
                    Rooms = result.items,
                    TotalCount = result.totalCount
                };
            }
            catch (Exception)
            {
                throw new ArgumentException("Unsupported specification type", nameof(specification));
            }
        }
    }
}
