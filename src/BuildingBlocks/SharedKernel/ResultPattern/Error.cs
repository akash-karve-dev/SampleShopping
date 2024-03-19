namespace SharedKernel.ResultPattern
{
    public record Error(string Code, string? Description = default)
    {
        public static Error None => new("");
        public static Error NullValue => new("Error.NullValue", "Null value was provided.");

        public static implicit operator Result(Error error) => Result.Failure(error);
    }
}