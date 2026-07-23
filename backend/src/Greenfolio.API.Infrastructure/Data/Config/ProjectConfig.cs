using Greenfolio.API.Core.GreenGraph;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenfolio.API.Infrastructure.Data.Config;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
  public void Configure(EntityTypeBuilder<Project> builder)
  {
    builder.ToTable("projects");
    builder.Property(p => p.SourceId).HasColumnName("source_id").IsRequired();
    builder.Property(p => p.SourceNativeId).HasColumnName("source_native_id").IsRequired();
    builder.Property(p => p.Title).HasColumnName("title").IsRequired();
    builder.Property(p => p.StartDate).HasColumnName("start_date");
    builder.Property(p => p.EndDate).HasColumnName("end_date");
    builder.Property(p => p.FundingAmount).HasColumnName("funding_amount").HasColumnType("numeric(14,2)");
    builder.Property(p => p.Currency).HasColumnName("currency").HasMaxLength(3);
    builder.Property(p => p.Program).HasColumnName("program").HasMaxLength(100);
    builder.Property(p => p.OurSummary).HasColumnName("our_summary");
    builder.Property(p => p.SourceUrl).HasColumnName("source_url").IsRequired();
    builder.Property<string>("SearchVector").HasColumnName("search_vector")
      .HasColumnType("tsvector")
      .HasComputedColumnSql("to_tsvector('english', coalesce(title, ''))", stored: true);
    builder.HasIndex("SearchVector").HasMethod("GIN");
    builder.HasIndex(p => new { p.SourceId, p.SourceNativeId }).IsUnique();
  }
}

public class ProjectParticipantConfiguration : IEntityTypeConfiguration<ProjectParticipant>
{
  public void Configure(EntityTypeBuilder<ProjectParticipant> builder)
  {
    builder.ToTable("project_participants");
    builder.Property(p => p.ProjectId).HasColumnName("project_id").IsRequired();
    builder.Property(p => p.OrgId).HasColumnName("org_id").IsRequired();
    builder.Property(p => p.Role).HasColumnName("role").HasConversion<string>().HasMaxLength(20);
    builder.Property(p => p.PersonId).HasColumnName("person_id");
    builder.HasIndex(p => new { p.ProjectId, p.OrgId, p.Role }).IsUnique();
  }
}

public class ProjectTopicConfiguration : IEntityTypeConfiguration<ProjectTopic>
{
  public void Configure(EntityTypeBuilder<ProjectTopic> builder)
  {
    builder.ToTable("project_topics");
    builder.Property(p => p.ProjectId).HasColumnName("project_id").IsRequired();
    builder.Property(p => p.TopicId).HasColumnName("topic_id").IsRequired();
    builder.Property(p => p.Confidence).HasColumnName("confidence");
    builder.Property(p => p.ClassifierVersion).HasColumnName("classifier_version").HasMaxLength(50);
    builder.HasIndex(p => new { p.ProjectId, p.TopicId }).IsUnique();
  }
}

public class OrgTopicConfiguration : IEntityTypeConfiguration<OrgTopic>
{
  public void Configure(EntityTypeBuilder<OrgTopic> builder)
  {
    builder.ToTable("org_topics");
    builder.Property(o => o.OrgId).HasColumnName("org_id").IsRequired();
    builder.Property(o => o.TopicId).HasColumnName("topic_id").IsRequired();
    builder.Property(o => o.Score).HasColumnName("score");
    builder.HasIndex(o => new { o.OrgId, o.TopicId }).IsUnique();
  }
}

public class CollabEdgeConfiguration : IEntityTypeConfiguration<CollabEdge>
{
  public void Configure(EntityTypeBuilder<CollabEdge> builder)
  {
    builder.ToTable("collab_edges");
    builder.Property(c => c.OrgA).HasColumnName("org_a").IsRequired();
    builder.Property(c => c.OrgB).HasColumnName("org_b").IsRequired();
    builder.Property(c => c.Weight).HasColumnName("weight");
    builder.Property(c => c.FirstYear).HasColumnName("first_year");
    builder.Property(c => c.LastYear).HasColumnName("last_year");
    builder.Property(c => c.ProjectCount).HasColumnName("project_count");
    builder.HasIndex(c => new { c.OrgA, c.OrgB }).IsUnique();
  }
}
