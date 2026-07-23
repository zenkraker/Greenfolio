using System.Reflection;
using Ardalis.SharedKernel;
using Greenfolio.API.Core.GreenGraph;
using Microsoft.EntityFrameworkCore;

namespace Greenfolio.API.Infrastructure.Data;
public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }

  public DbSet<Source> Sources => Set<Source>();
  public DbSet<Organization> Organizations => Set<Organization>();
  public DbSet<OrgSourceRecord> OrgSourceRecords => Set<OrgSourceRecord>();
  public DbSet<OrgMerge> OrgMerges => Set<OrgMerge>();
  public DbSet<Person> Persons => Set<Person>();
  public DbSet<Field> Fields => Set<Field>();
  public DbSet<Topic> Topics => Set<Topic>();
  public DbSet<EuroSciVocCrosswalk> EuroSciVocCrosswalks => Set<EuroSciVocCrosswalk>();
  public DbSet<Project> Projects => Set<Project>();
  public DbSet<ProjectParticipant> ProjectParticipants => Set<ProjectParticipant>();
  public DbSet<ProjectTopic> ProjectTopics => Set<ProjectTopic>();
  public DbSet<OrgTopic> OrgTopics => Set<OrgTopic>();
  public DbSet<CollabEdge> CollabEdges => Set<CollabEdge>();
  public DbSet<Publication> Publications => Set<Publication>();
  public DbSet<PublicationAuthor> PublicationAuthors => Set<PublicationAuthor>();
  public DbSet<Claim> Claims => Set<Claim>();
  public DbSet<User> Users => Set<User>();
  public DbSet<CollabRequest> CollabRequests => Set<CollabRequest>();
  public DbSet<ScoutingLead> ScoutingLeads => Set<ScoutingLead>();
  public DbSet<Event> Events => Set<Event>();
  public DbSet<SuppressionList> SuppressionList => Set<SuppressionList>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
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
