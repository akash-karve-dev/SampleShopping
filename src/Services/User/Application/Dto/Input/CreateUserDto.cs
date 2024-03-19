namespace User.Application.Dto.Input
{
    public record CreateUserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}