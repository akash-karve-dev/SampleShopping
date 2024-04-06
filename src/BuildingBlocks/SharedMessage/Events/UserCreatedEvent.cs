namespace SharedMessage.Events
{
    public record UserCreatedEvent
    {
        public Guid UserId { get; init; }

        public string? Name { get; init; }

        public string? Email { get; init; }
    }
}