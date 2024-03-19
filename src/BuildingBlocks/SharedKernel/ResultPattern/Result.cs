namespace SharedKernel.ResultPattern
{
    public record Result(bool IsSuccess, Error Error)
    {
        public bool IsFailure => !IsSuccess;

        public static Result Success() => new(true, Error.None);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, false, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }

    public record Result<TValue> : Result
    {
        private TValue? _value { get; }

        public Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue? Value => IsSuccess ? _value : throw new InvalidOperationException("Value for failure result cannot be accessed");

        public static implicit operator Result<TValue>(TValue value) => value != null ? value : Failure<TValue>(Error.NullValue);
    }
}