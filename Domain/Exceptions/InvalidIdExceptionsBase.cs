namespace Domain.Exceptions
{
    public abstract class InvalidIdExceptionBase : RoomAsyncExpection
    {
        protected InvalidIdExceptionBase()
        {

        }

        public static void Throw<T>(T id)
            => throw new InvalidIdException<T>(id);
    }

    public class InvalidIdException<TValue> : InvalidIdExceptionBase
    {
        public InvalidIdException(TValue? id)
            => Id = id;

        public TValue? Id { get; init; }
    }
}
