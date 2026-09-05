using Academia.API.Middleware;
using Academia.Domain.Entities;
using Academia.InfraIoC;
using Academia.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddInfrastructureSwagger();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BancoContext>();
    await context.Database.MigrateAsync();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
        if (!roleResult.Succeeded)
        {
            var erros = string.Join(
                "; ",
                roleResult.Errors.Select(e => e.Description));

            throw new InvalidOperationException(
                $"Não foi possível criar a role Admin: {erros}");
        }

    }

    var adminEmail = builder.Configuration["Admin:Email"];
    var adminPassword = builder.Configuration["Admin:Password"];

    if (string.IsNullOrWhiteSpace(adminEmail) ||
    string.IsNullOrWhiteSpace(adminPassword))
    {
        throw new InvalidOperationException(
            "As credenciais do administrador não foram configuradas.");
    }
    var adminExistente = await userManager.FindByEmailAsync(adminEmail);

    if (adminExistente is null)
    {
        var admin = new ApplicationUser
        {
            UserName = "Administrador",
            Email = adminEmail,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        var resultado = await userManager.CreateAsync(admin, adminPassword);
        if (!resultado.Succeeded)
        {
            var erros = string.Join(
                "; ",
                resultado.Errors.Select(e => e.Description));

            throw new InvalidOperationException(
                $"Não foi possível criar o administrador: {erros}");
        }
        await userManager.SetLockoutEnabledAsync(admin, false);
        var roleResult =
            await userManager.AddToRoleAsync(admin, "Admin");

        if (!roleResult.Succeeded)
        {
            var erros = string.Join(
                "; ",
                roleResult.Errors.Select(e => e.Description));

            throw new InvalidOperationException(
                $"Não foi possível adicionar o administrador à role: {erros}");
        }
    }
}

app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Academia v1");
    });


app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
