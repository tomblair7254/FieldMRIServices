using FieldMRIServices.Data;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Extensions.Services;
using FieldMRIServices.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5247");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<IInventoryServices, InventoryServices>();
builder.Services.AddScoped<IComputerService, ComputerService>();
builder.Services.AddScoped<IUserLogsService, UserLogsService>(); // Register IUserLogsService
builder.Services.AddScoped<UserLogsService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<FieldMRIServicesServices.Data.TokenProvider>();

var connectionString = builder.Configuration.GetConnectionString("FMIconnection")
          ?? throw new InvalidOperationException("Connection 'FMIventoryDbContext' not found");

builder.Services.AddDbContext<FMIventoryDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    // Remove default password validators
    options.Password.RequiredLength = 0;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
})
.AddRoles<ApplicationRoles>()
.AddEntityFrameworkStores<FMIventoryDbContext>()
.AddPasswordValidator<CustomPasswordValidator<IdentityUser>>(); // Add custom password validator

builder.Services.AddScoped<RoleManager<ApplicationRoles>>();
builder.Services.AddScoped<IRoleStore<ApplicationRoles>, RoleStore<ApplicationRoles, FMIventoryDbContext, string>>();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register SessionService
builder.Services.AddHttpContextAccessor();
// builder.Services.AddScoped<SessionService>();

// Configure the file upload size limit
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10485760; // 10 MB
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cWWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFiW39fcXVWRGNcU0BwWw==");


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;
            await context.Response.WriteAsync("An exception was thrown.");

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                await context.Response.WriteAsync(" The file was not found.");
            }

            if (exceptionHandlerPathFeature?.Path == "/")
            {
                await context.Response.WriteAsync(" Page: Home.");
            }
        });
    });
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Use session middleware
app.UseSession();

// Use custom middleware for logging user activity
app.UseMiddleware<UserActivityMiddleware>();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Handling request: {Path}", context.Request.Path);
    await next.Invoke();
    logger.LogInformation("Finished handling request.");
});

app.Use(async (context, next) =>
{
    if (!context.User.Identity.IsAuthenticated && !context.Request.Path.StartsWithSegments("/Identity/Account/Login"))
    {
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }
    await next();
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
{
    public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
    {
        var errors = new List<IdentityError>();

        if (password.Length < 6)
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordTooShort",
                Description = "Passwords must be at least 6 characters."
            });
        }

        if (password.Length > 12)
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordTooLong",
                Description = "Passwords must be at most 12 characters."
            });
        }

        if (!password.Any(char.IsDigit))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordRequiresDigit",
                Description = "Passwords must have at least one digit ('0'-'9')."
            });
        }

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Passwords must have at least one non alphanumeric character."
            });
        }

        return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
    }
}