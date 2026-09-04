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

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
   
    var adminEmail = "admin@academia.com";
    var adminExistente = await userManager.FindByEmailAsync(adminEmail);

    if (adminExistente is null)
    {
        var admin = new AplicationUser
        {
            UserName = "Administrador",
            Email = adminEmail,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var resultado = await userManager.CreateAsync(admin, "@Administrador123");

        if (resultado.Succeeded)
        {
            await userManager.SetLockoutEnabledAsync(admin, false);
            await userManager.AddToRoleAsync(admin, "Admin");
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
