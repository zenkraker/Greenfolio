using Greenfolio.API.Core.GreenGraph;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenfolio.API.Infrastructure.Data.Config;

public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
  public void Configure(EntityTypeBuilder<Source> builder)
  {
    builder.ToTable("sources");
    builder.Property(s => s.Key).HasColumnName("key").HasMaxLength(60).IsRequired();
    builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).IsRequired();
    builder.Property(s => s.Licence).HasColumnName("licence").HasMaxLength(120).IsRequired();
    builder.Property(s => s.AttributionRequired).HasColumnName("attribution_required");
    builder.Property(s => s.Url).HasColumnName("url");
    builder.HasIndex(s => s.Key).IsUnique();
  }
}

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
  public void Configure(EntityTypeBuilder<Claim> builder)
  {
    builder.ToTable("claims");
    builder.Property(c => c.OrgId).HasColumnName("org_id").IsRequired();
    builder.Property(c => c.UserId).HasColumnName("user_id").IsRequired();
    builder.Property(c => c.Method).HasColumnName("method").HasConversion<string>().HasMaxLength(20);
    builder.Property(c => c.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20);
    builder.Property(c => c.CreatedAt).HasColumnName("created_at");
  }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");
    builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
    builder.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();
    builder.Property(u => u.Role).HasColumnName("role").HasConversion<string>().HasMaxLength(20);
    builder.Property(u => u.OrgId).HasColumnName("org_id");
    builder.HasIndex(u => u.Email).IsUnique();
  }
}

public class CollabRequestConfiguration : IEntityTypeConfiguration<CollabRequest>
{
  public void Configure(EntityTypeBuilder<CollabRequest> builder)
  {
    builder.ToTable("collab_requests");
    builder.Property(c => c.FromName).HasColumnName("from_name").IsRequired();
    builder.Property(c => c.FromEmail).HasColumnName("from_email").IsRequired();
    builder.Property(c => c.FromCompany).HasColumnName("from_company").IsRequired();
    builder.Property(c => c.TargetOrgId).HasColumnName("target_org_id");
    builder.Property(c => c.TopicId).HasColumnName("topic_id");
    builder.Property(c => c.Description).HasColumnName("description").IsRequired();
    builder.Property(c => c.ConsentAt).HasColumnName("consent_at");
    builder.Property(c => c.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20);
    builder.Property(c => c.CreatedAt).HasColumnName("created_at");
  }
}

public class ScoutingLeadConfiguration : IEntityTypeConfiguration<ScoutingLead>
{
  public void Configure(EntityTypeBuilder<ScoutingLead> builder)
  {
    builder.ToTable("scouting_leads");
    builder.Property(s => s.Name).HasColumnName("name").IsRequired();
    builder.Property(s => s.Email).HasColumnName("email").IsRequired();
    builder.Property(s => s.Company).HasColumnName("company").IsRequired();
    builder.Property(s => s.Brief).HasColumnName("brief").IsRequired();
    builder.Property(s => s.BudgetBand).HasColumnName("budget_band").HasMaxLength(50);
    builder.Property(s => s.ConsentAt).HasColumnName("consent_at");
    builder.Property(s => s.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20);
    builder.Property(s => s.CreatedAt).HasColumnName("created_at");
  }
}

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.ToTable("events");
    builder.Property(e => e.OccurredAt).HasColumnName("occurred_at");
    builder.Property(e => e.Type).HasColumnName("type").HasConversion<string>().HasMaxLength(20);
    builder.Property(e => e.OrgId).HasColumnName("org_id");
    builder.Property(e => e.TopicId).HasColumnName("topic_id");
    builder.Property(e => e.QueryText).HasColumnName("query_text");
    builder.Property(e => e.MetaJson).HasColumnName("meta").HasColumnType("jsonb");
    builder.HasIndex(e => e.OccurredAt);
  }
}

public class SuppressionListConfiguration : IEntityTypeConfiguration<SuppressionList>
{
  public void Configure(EntityTypeBuilder<SuppressionList> builder)
  {
    builder.ToTable("suppression_list");
    builder.Property(s => s.Matcher).HasColumnName("matcher").HasConversion<string>().HasMaxLength(20);
    builder.Property(s => s.Value).HasColumnName("value").IsRequired();
    builder.Property(s => s.Reason).HasColumnName("reason").IsRequired();
    builder.Property(s => s.CreatedAt).HasColumnName("created_at");
    builder.HasIndex(s => new { s.Matcher, s.Value }).IsUnique();
  }
}
