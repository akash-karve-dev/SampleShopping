namespace User.Application.Dto.Output
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}