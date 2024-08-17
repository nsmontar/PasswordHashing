using Microsoft.EntityFrameworkCore;
using Web.Api.Database;
using Web.Api.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.CustomSchemaIds(id => id.FullName!.Replace('+', '-')));
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("db"));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<UserRegistrationService>();
builder.Services.AddScoped<UserLoginService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

UserEndpoints.Map(app);

app.Run();
