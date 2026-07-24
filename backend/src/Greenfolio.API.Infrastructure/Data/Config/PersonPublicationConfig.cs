using Greenfolio.API.Core.GreenGraph;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenfolio.API.Infrastructure.Data.Config;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
  public void Configure(EntityTypeBuilder<Person> builder)
  {
    builder.ToTable("persons");
    builder.Property(p => p.Orcid).HasColumnName("orcid").HasMaxLength(30);
    builder.Property(p => p.FullName).HasColumnName("full_name").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).IsRequired();
    builder.Property(p => p.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20);
    builder.HasIndex(p => p.Orcid).IsUnique().HasFilter("orcid IS NOT NULL");
  }
}

public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
{
  public void Configure(EntityTypeBuilder<Publication> builder)
  {
    builder.ToTable("publications");
    builder.Property(p => p.OpenAlexId).HasColumnName("openalex_id").IsRequired();
    builder.Property(p => p.Title).HasColumnName("title").IsRequired();
    builder.Property(p => p.Year).HasColumnName("year");
    builder.Property(p => p.Doi).HasColumnName("doi").HasMaxLength(200);
    builder.HasIndex(p => p.OpenAlexId).IsUnique();
  }
}

public class PublicationAuthorConfiguration : IEntityTypeConfiguration<PublicationAuthor>
{
  public void Configure(EntityTypeBuilder<PublicationAuthor> builder)
  {
    builder.ToTable("publication_authors");
    builder.Property(a => a.PublicationId).HasColumnName("publication_id").IsRequired();
    builder.Property(a => a.PersonId).HasColumnName("person_id");
    builder.Property(a => a.OrgId).HasColumnName("org_id");
    builder.HasIndex(a => new { a.PublicationId, a.PersonId, a.OrgId }).IsUnique();
  }
}
