using Domain.Error;

namespace Domain
{
    /// <summary>
    /// An implementation of the Result Pattern, a result type that holds either a value or an error.
    /// Prefer this method over throwing exception for Use Case definable errors traversing the Ports of the Core Domain, i.e. Driver & Driven Ports of the Hexagon.
    /// <see cref="VoidResult{TError}"/>
    /// <seealso href="https://www.youtube.com/watch?v=cKa6nCuh7cc"> Learning resource 1</seealso>
    /// <seealso href="https://youtu.be/LXF-rRWaIxc"> Learning resource 2</seealso>
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned on success.</typeparam>
    /// <typeparam name="TError">The type of error returned on failure.</typeparam>
    public readonly struct Result<TValue, TError> where TError : IRoomAsyncError
    {
        private readonly TValue? _value;
        private readonly TError? _error;
        private readonly bool _success;

        public bool TryGetValue(out TValue value)
        {
            value = _value!;
            return _success;
        }

        public bool TryGetError(out TError error)
        {
            error = _error!;
            return !_success;
        }

        public static implicit operator Result<TValue, TError>(TValue value) => Success(value);

        public static implicit operator Result<TValue, TError>(TError error) => Failure(error);

        private Result(TValue value)
        {
            _value = value;
            _error = default;
            _success = true;
        }
        private Result(TError error)
        {
            _value = default;
            _error = error;
            _success = false;
        }

        private static Result<TValue, TError> Success(TValue value) => new Result<TValue, TError>(value);
        private static Result<TValue, TError> Failure(TError error) => new Result<TValue, TError>(error);
    }
}
