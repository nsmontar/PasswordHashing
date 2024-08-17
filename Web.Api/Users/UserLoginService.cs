using Web.Api.Database;

namespace Web.Api.Users;

public sealed class UserLoginService(UserRepository userRepository, PasswordHasher passwordHasher)
{
    public sealed record Request(string Email, string Password);
    
    public async Task<User> VerifyLoginCredentials(Request request)
    {
        User? user = await userRepository.GetByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new Exception("User with provided email doesn't exist.");
        }
        
        bool verified = passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        
        if (!verified)
        {
            throw new Exception("Invalid password.");
        }

        return user;
    } 
}