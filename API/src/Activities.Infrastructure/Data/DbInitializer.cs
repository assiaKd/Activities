using Activities.Domain;
using Activities.Infrastructure.Data.Seed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Activities.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(
        AppDbContext context,
        UserManager<User> userManager,
        IHostEnvironment env)
    {
        if (!userManager.Users.Any())
        {
            var users = await LoadAsync<UserSeedModel>(env, "Seed/Data/users.json");

            foreach (var u in users)
            {
                var user = new User
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    UserName = u.Email,
                    Email = u.Email
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }

        if (context.Activities.Any()) return;

        var seedActivities = await LoadAsync<ActivitySeedModel>(env, "Seed/Data/activities.json");

        var activities = seedActivities.Select(a => new Activity
        {
            Title = a.Title,
            Date = DateTime.UtcNow.AddMonths(a.DateOffsetMonths),
            Description = a.Description,
            Category = a.Category,
            City = a.City,
            Venue = a.Venue,
            Latitude = a.Latitude,
            Longitude = a.Longitude,
            Attendees = a.Attendees.Select(at => new ActivityAttendee
            {
                UserId = at.UserId,
                IsHost = at.IsHost
            }).ToList()
        });

        context.Activities.AddRange(activities);

        await context.SaveChangesAsync();
    }

    private static async Task<List<T>> LoadAsync<T>(IHostEnvironment env, string path)
    {
        var assemblyPath = Path.GetDirectoryName(typeof(InfrastructureServices).Assembly.Location);
        var filePath = Path.Combine(assemblyPath, "Data", path);

        var json = await File.ReadAllTextAsync(filePath);

        return JsonSerializer.Deserialize<List<T>>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];
    }
}