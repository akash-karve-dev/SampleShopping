namespace User.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public static User CreateUser(string name, string email)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email
            };
        }
    }
}