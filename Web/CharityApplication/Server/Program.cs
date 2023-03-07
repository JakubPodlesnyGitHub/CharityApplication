using Application;
using CharityApplication.Server.ApiFilters;
using CharityApplication.Server.Hubs;
using CharityApplication.Server.ServerConfigurations;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.ServicesConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");

builder.Services.AddDbContext<CharityApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Account, IdentityRole<int>>()
            .AddEntityFrameworkStores<CharityApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddApplicationConfiguration();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddSingalRConfiguration();
var app = builder.Build();

app.UseResponseCompression();
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatHub>("/chathub");
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();