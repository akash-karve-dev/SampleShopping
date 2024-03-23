using SharedKernel.ResultPattern;

namespace User.Domain.User
{
    public class UserErrors
    {
        public static Error NotFound(Guid id) => new("User.NotFound", $"User not found for Id: [{id}]");
        public static Error AlreadyExist(Guid id) => new("User.AlreadyExist", $"User already exists for Id: [{id}]");
    }
}