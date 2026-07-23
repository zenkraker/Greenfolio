using Greenfolio.API.Core.GreenGraph;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;

namespace Greenfolio.API.Infrastructure.Data.Config;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
  public void Configure(EntityTypeBuilder<Organization> builder)
  {
    builder.ToTable("organizations");
    builder.Property(o => o.RorId).HasColumnName("ror_id");
    builder.Property(o => o.Name).HasColumnName("name").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).IsRequired();
    builder.Property(o => o.NameVariants).HasColumnName("name_variants").HasColumnType("text[]");
    builder.Property(o => o.Country).HasColumnName("country").HasMaxLength(2).IsRequired();
    builder.Property(o => o.OrgType).HasColumnName("org_type").HasConversion<string>().HasMaxLength(20);
    builder.Property(o => o.Lat).HasColumnName("lat");
    builder.Property(o => o.Lng).HasColumnName("lng");
    builder.Property(o => o.Website).HasColumnName("website");
    builder.Property(o => o.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20);
    builder.Property<NpgsqlTsVector>("SearchVector").HasColumnName("search_vector")
      .HasColumnType("tsvector")
      .HasComputedColumnSql("to_tsvector('english', coalesce(name, ''))", stored: true);
    builder.HasIndex("SearchVector").HasMethod("GIN");
    builder.HasIndex(o => o.RorId).IsUnique().HasFilter("ror_id IS NOT NULL");
  }
}

public class OrgSourceRecordConfiguration : IEntityTypeConfiguration<OrgSourceRecord>
{
  public void Configure(EntityTypeBuilder<OrgSourceRecord> builder)
  {
    builder.ToTable("org_source_records");
    builder.Property(r => r.OrgId).HasColumnName("org_id");
    builder.Property(r => r.SourceId).HasColumnName("source_id").IsRequired();
    builder.Property(r => r.SourceNativeId).HasColumnName("source_native_id").IsRequired();
    builder.Property(r => r.RawJson).HasColumnName("raw").HasColumnType("jsonb").IsRequired();
    builder.Property(r => r.ImportedAt).HasColumnName("imported_at");
    builder.HasIndex(r => new { r.SourceId, r.SourceNativeId });
    builder.HasOne<Organization>().WithMany(o => o.SourceRecords).HasForeignKey(r => r.OrgId);
  }
}

public class OrgMergeConfiguration : IEntityTypeConfiguration<OrgMerge>
{
  public void Configure(EntityTypeBuilder<OrgMerge> builder)
  {
    builder.ToTable("org_merges");
    builder.Property(m => m.KeptOrgId).HasColumnName("kept_org_id").IsRequired();
    builder.Property(m => m.MergedOrgId).HasColumnName("merged_org_id").IsRequired();
    builder.Property(m => m.Method).HasColumnName("method").HasConversion<string>().HasMaxLength(30);
    builder.Property(m => m.Confidence).HasColumnName("confidence");
    builder.Property(m => m.DecidedBy).HasColumnName("decided_by");
    builder.Property(m => m.DecidedAt).HasColumnName("decided_at");
    builder.HasIndex(m => m.MergedOrgId).IsUnique();
  }
}
