namespace User.Application.Dto.Output
{
    public record UserResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}