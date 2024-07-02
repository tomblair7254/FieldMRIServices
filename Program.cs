using FieldMRIServices.Data;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Extensions.Services;
using FieldMRIServices.Services;
using FieldMRIServicesServices.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();



builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<IIventoryServices, InventoryServices>();
builder.Services.AddScoped<IComputerServices, ComputerServices>();
builder.Services.AddScoped<IModiskService, ModiskService>();
builder.Services.AddScoped<IHarddriveService, HarddriveService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IMemoryService, MemoryService>();
builder.Services.AddScoped<IFiberService, FiberService>();
builder.Services.AddScoped<IMonitorService, MonitorService>();
builder.Services.AddScoped<IIEEEService, IEEEServive>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<TokenProvider>();

//builder.Services.AddSyncfusionBlazor();

var connectionString = builder.Configuration.GetConnectionString("FMIconnection")
          ?? throw new InvalidOperationException("Connection 'FMIventoryDbContext' not found");

builder.Services.AddDbContext<FMIventoryDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FMIventoryDbContext>();



var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmtCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXhecXVSRmNZUkR3WUA=");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // using static System.Net.Mime.MediaTypeNames;
            context.Response.ContentType = Text.Plain;

            await context.Response.WriteAsync("An exception was thrown.");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                await context.Response.WriteAsync(" The file was not found.");
            }

            if (exceptionHandlerPathFeature?.Path == "/")
            {
                await context.Response.WriteAsync(" Page: Home.");
            }
        });
    }); app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
