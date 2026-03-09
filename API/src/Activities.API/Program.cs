using Activities.Application.Activities.Queries;
using Activities.Application.Core;
using Activities.Application.Interfaces;
using Activities.Domain;
using Activities.Infrastructure;
using Activities.Infrastructure.Data;
using Activities.Infrastructure.Repositories;
using Activities.Presentation;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdentityApiEndpoints<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetActivityList>();
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = builder.Configuration["Licences:MediatR"];
}, typeof(MappingProfiles));

var app = builder.Build();

await InfrastructureServices.ApplyMigrationsAsync(app.Services);

ActivitiesEndPoint.MapActivitiesEndpoints(app);
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.Services.InitializeDatabaseAsync();
}

app.UseHttpsRedirection();
app.Run();

