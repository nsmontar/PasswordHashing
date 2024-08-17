namespace Web.Api.Users;

public class UserEndpoints
{
    public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("register",
            async (UserRegistrationService.Request request, UserRegistrationService userRegistrationService) =>
                await userRegistrationService.Register(request));

        builder.MapPost("login",
            async (UserLoginService.Request request, UserLoginService userLoginService) =>
                await userLoginService.VerifyLoginCredentials(request));
            
        return builder;
    }
}
