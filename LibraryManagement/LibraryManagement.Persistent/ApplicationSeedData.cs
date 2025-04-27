using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Persistent;

public static class ApplicationSeedData
{
    public static IApplicationBuilder InitialData(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<IApplicationBuilder>>();

        try
        {
            logger.LogInformation("Attempting database seeding...");

            // Run the async seeding method synchronously during startup.
            // While generally discouraged, it's often acceptable for one-time startup tasks.
            // Ensure your application startup can handle this potential blocking.
            SeedDatabaseAsync(services).GetAwaiter().GetResult();

            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during database seeding.");
        }

        return app;
    }

    private static async Task SeedDatabaseAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        ArgumentNullException.ThrowIfNull(context, nameof(context));
        await context.Database.EnsureCreatedAsync();

        InitialUser(services);
        InitialBookData(context);
    }

    private static void InitialUser(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
        ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));
        if (!roleManager.Roles.Any())
        {
            _ = roleManager.CreateAsync(new Role { Name = Roles.SupperAdmin, Description = "Supper Admin" }).Result;
            _ = roleManager.CreateAsync(new Role { Name = Roles.Member, Description = "Member" }).Result;
        }

        if (!userManager.Users.Any())
        {
            var userLogin = userManager.FindByEmailAsync("testuser@example.com").Result;

            if (userLogin == null)
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = "testuser@example.com",
                    FirstName = "testuser",
                    LastName = "Last Name",
                    EmailConfirmed = true,
                    Email = "testuser@example.com"
                };
                _ = userManager.CreateAsync(user, "Abc@12345").Result;
                var userDb = userManager.FindByEmailAsync("testuser@example.com").Result;
                var userRole = userManager.AddToRolesAsync(userDb, [Roles.SupperAdmin]).Result;
            }

            var memberUserLogin = userManager.FindByEmailAsync("memberuser@example.com").Result;

            if (memberUserLogin is null)
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = "memberuser@example.com",
                    EmailConfirmed = true,
                    FirstName = "memberuser",
                    LastName = "Last Name",
                    Email = "memberuser@example.com"
                };

                _ = userManager.CreateAsync(user, "Abc@12345").Result;
                var userDb = userManager.FindByEmailAsync("memberuser@example.com").Result;
                var userRole = userManager.AddToRolesAsync(userDb, [Roles.Member]).Result;
            }
        }
    }

    private static void InitialBookData(ApplicationDbContext context)
    {
        if (context.Categories.Any())
        {
            context.Categories.AddRange(new Category() { Name = "Horror" }, new Category() { Name = "Fantasy" }, new Category() { Name = "Mystery" });
        }

        if (!context.Books.Any())
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    PublicationDate = new DateTime(1979, 01, 01, 0, 0, 0)
                },
                new Book { Title = "Pride and Prejudice", PublicationDate = new DateTime(1979, 01, 01, 0, 0, 0) },
                new Book { Title = "1984", PublicationDate = new DateTime(1979, 01, 01, 0, 0, 0) },
                new Book { Title = "To Kill a Mockingbird", PublicationDate = new DateTime(1979, 01, 01, 0, 0, 0) },
                new Book { Title = "The Great Gatsby", PublicationDate = new DateTime(1979, 01, 01, 0, 0, 0) }
            );
        }

        context.SaveChanges();
    }
}