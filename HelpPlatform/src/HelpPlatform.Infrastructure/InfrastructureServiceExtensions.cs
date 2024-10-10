using Ardalis.GuardClauses;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.Contributor.Interfaces;
using HelpPlatform.Core.Contributor.Services;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.Core.DonationRequestDomain.Services;
using HelpPlatform.Core.UserDomain.Interfaces;
using HelpPlatform.Core.UserDomain.Services;
using HelpPlatform.Infrastructure.Data;
using HelpPlatform.Infrastructure.Data.Queries;
using HelpPlatform.Infrastructure.Email;
using HelpPlatform.UseCases.Contributors.List;
using HelpPlatform.Core.NotificationDomain.Services;
using HelpPlatform.Core.NotificationDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HelpPlatform.Infrastructure;

public static class InfrastructureServiceExtensions {
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger) {
        string? connectionString = config.GetConnectionString("SqliteConnection");
        Guard.Against.Null(connectionString);
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped<IListContributorsQueryService, ListContributorsQueryService>();
        services.AddScoped<IDeleteContributorService, DeleteContributorService>();
        services.AddScoped<IDeleteUserService, DeleteUserService>();
        services.AddScoped<IUpdateUserService, UpdateUserService>();
        services.AddScoped<ICreateDonationRequestClaimService, CreateDonationRequestClaimService>();
        services.AddScoped<INotificationService, NotificationService>();

        services.Configure<MailserverConfiguration>(config.GetSection("Mailserver"));

        logger.LogInformation("{Project} services registered", "Infrastructure");

        return services;
    }
}
