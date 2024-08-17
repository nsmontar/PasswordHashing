namespace Web.Api.Users;

public record User(
    Guid Id, 
    string Email, 
    string? FirstName, 
    string? LastName, 
    string PasswordHash
    );