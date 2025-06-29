using Domain.Error;

namespace Domain
{
    public readonly struct VoidResult<TError> where TError : IRoomAsyncError
    {
        private readonly TError? _error;
        private readonly bool _success;
        public bool TryGetError(out TError error)
        {
            error = _error!;
            return !_success;
        }
        public static implicit operator VoidResult<TError>(TError error) => Failure(error);
        public static implicit operator VoidResult<TError>(Success success) => Success(success);

        private VoidResult(TError error)
        {
            _error = error;
            _success = false;
        }

        private VoidResult(Success success)
        {
            _error = default;
            _success = true;
        }
        private static VoidResult<TError> Success(Success success) => new VoidResult<TError>(success);
        private static VoidResult<TError> Failure(TError error) => new VoidResult<TError>(error);
    }

    public struct Success;

    internal struct VoidResult
    {
        internal static Success Sucessful => new();
    }
}
