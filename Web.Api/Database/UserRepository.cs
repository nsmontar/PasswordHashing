using Microsoft.EntityFrameworkCore;
using Web.Api.Users;

namespace Web.Api.Database;

public sealed class UserRepository(AppDbContext context)
{
    public Task<bool> ExistsAsync(string email)
    {
        return context.Users.AnyAsync(user => user.Email == email);
    }

    public async Task InsertAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
    
    public Task<User?> GetByEmailAsync(string email)
    {
        return context.Users.SingleOrDefaultAsync(user => user.Email == email);
    }
}