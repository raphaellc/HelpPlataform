﻿using System.Reflection;
using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using FastEndpoints;
using FastEndpoints.Swagger;
using HelpPlatform.Core.Contributor.ContributorAggregate;
using HelpPlatform.Core.Contributor.Interfaces;
using HelpPlatform.Infrastructure;
using HelpPlatform.Infrastructure.Data;
using HelpPlatform.Infrastructure.Email;
using HelpPlatform.UseCases.Contributors.Create;
using MediatR;
using Serilog;
using Serilog.Extensions.Logging;

using HelpPlatform.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<HelpPlatform.Web.Program>();

// Configure Web Behavior
builder.Services.Configure<CookiePolicyOptions>(options => {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddFastEndpoints()
    .SwaggerDocument(o => { o.ShortSchemaNames = true; });

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"), b => 
        b.MigrationsAssembly("HelpPlatform.Web")));
        
ConfigureMediatR();

builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger);

if (builder.Environment.IsDevelopment()){
    // Use a local test email server
    // See: https://ardalis.com/configuring-a-local-test-email-server/
    builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();

    // Otherwise use this:
    //builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
    AddShowAllServicesSupport();
}
else{
    builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var authContext = services.GetRequiredService<ApplicationDbContext>();
        authContext.Database.Migrate(); 
    }
}
else{
    app.UseDefaultExceptionHandler(); // from FastEndpoints
    app.UseHsts();
}

app.UseFastEndpoints()
    .UseSwaggerGen(); // Includes AddFileServer and static files middleware

app.UseHttpsRedirection();

await SeedDatabase(app);

app.Run();

static async Task SeedDatabase(WebApplication app) {
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try{
        var context = services.GetRequiredService<AppDbContext>();
        //          context.Database.Migrate();
        context.Database.EnsureCreated();
        await SeedData.InitializeAsync(context);
    }
    catch (Exception ex){
        var logger = services.GetRequiredService<ILogger<HelpPlatform.Web.Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

void ConfigureMediatR() {
    var mediatRAssemblies = new[]
    {
        Assembly.GetAssembly(typeof(Contributor)), // Core
        Assembly.GetAssembly(typeof(CreateContributorCommand)) // UseCases
    };
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

void AddShowAllServicesSupport() {
    // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
    builder.Services.Configure<ServiceConfig>(config => {
        config.Services = new List<ServiceDescriptor>(builder.Services);

        // optional - default path to view services is /listallservices - recommended to choose your own path
        config.Path = "/listservices";
    });
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
namespace HelpPlatform.Web {
  public partial class Program {}
}
