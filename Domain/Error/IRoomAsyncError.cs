namespace Domain.Error
{
    public interface IRoomAsyncError
    {
        string Message { get; }
    }

    /// <summary>
    /// This type serves as the base type for all use case defined errors in the RoomAsync System Domain.
    /// It is commonly used with Result Pattern <see cref="Result{TValue,TError}"/> to represent an error."/>
    /// </summary>
    public abstract class RoomAsyncError : IRoomAsyncError
    {
        protected RoomAsyncError(string message)
        {
            Message = message;
        }
        protected RoomAsyncError()
        {
        }

        public string Message { get; protected init; } = string.Empty;
    }
}
