using SharedKernel.ResultPattern;

namespace User.Domain
{
    public class UserErrors
    {
        public static Error NotFound(Guid id) => new("User.NotFound", $"User not found for Id: [{id}]");
    }
}