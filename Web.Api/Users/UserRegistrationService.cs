using Web.Api.Database;

namespace Web.Api.Users;

public sealed class UserRegistrationService(UserRepository userRepository, PasswordHasher passwordHasher)
{
    public sealed record Request(string Email, string FirstName, string LastName, string Password);
    
    public async Task<User> Register(Request request)
    {
        if (await userRepository.ExistsAsync(request.Email))
        {
            throw new Exception("User with provided email already exists.");
        }
        var user = new User(
            Id: Guid.NewGuid(),
            Email: request.Email,
            FirstName: request.FirstName,
            LastName: request.LastName,
            PasswordHash: passwordHasher.HashPassword(request.Password)
        );

        await userRepository.InsertAsync(user);

        return user;
    }
}