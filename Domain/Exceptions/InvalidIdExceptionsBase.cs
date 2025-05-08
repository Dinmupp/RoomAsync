namespace Domain.Exceptions
{
    public abstract class InvalidIdExceptionBase : RoomAsyncExpection
    {
        protected InvalidIdExceptionBase(string message) : base(message)
        {

        }

        public static void Throw<T>(T id)
            => throw new InvalidIdException<T>(id);
    }

    public class InvalidIdException<TValue> : InvalidIdExceptionBase
    {
        public InvalidIdException(TValue? id) : base($"Id of type {typeof(TValue).Name} with value {id?.ToString() ?? "null"} is invalid!")
            => Id = id;

        public TValue? Id { get; init; }
    }
}
