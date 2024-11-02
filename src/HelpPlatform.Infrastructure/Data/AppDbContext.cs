using System.Reflection;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.Contributor.ContributorAggregate;
using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.Core.ResourceTypeDomain;
using HelpPlatform.Core.UserDomain;
using Microsoft.EntityFrameworkCore;

namespace HelpPlatform.Infrastructure.Data;

public class AppDbContext : DbContext {
    private readonly IDomainEventDispatcher? _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options,
        IDomainEventDispatcher? dispatcher)
        : base(options) {
        _dispatcher = dispatcher;
    }

    public DbSet<Contributor> Contributors => Set<Contributor>();

    public DbSet<User> Users => Set<User>();
    
    public DbSet<DonationRequest> DonationRequests => Set<DonationRequest>();

    public DbSet<DonationRequestClaim> DonationRequestClaims => Set<DonationRequestClaim>();

    public DbSet<ResourceType> ResourceTypes => Set<ResourceType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_dispatcher == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
