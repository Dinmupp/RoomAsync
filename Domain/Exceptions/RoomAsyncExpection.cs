using Domain.Error;
namespace Domain.Exceptions
{
    /// <summary>
    /// This type serves as the base type for all defined Exceptions in the RoomAsync System Domain.
    /// Derive from this to represent and throw exceptions in the RoomAsync System Domain.
    /// For definable and expected errors use instead <see cref="RoomAsyncError"/>.
    /// </summary>
    public class RoomAsyncExpection : ApplicationException
    {
        protected RoomAsyncExpection(string message) : base(message)
        {
        }
    }
}
