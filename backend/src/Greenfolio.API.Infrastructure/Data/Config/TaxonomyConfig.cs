using Greenfolio.API.Core.GreenGraph;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenfolio.API.Infrastructure.Data.Config;

public class FieldConfiguration : IEntityTypeConfiguration<Field>
{
  public void Configure(EntityTypeBuilder<Field> builder)
  {
    builder.ToTable("fields");
    builder.Property(f => f.Key).HasColumnName("key").HasMaxLength(60).IsRequired();
    builder.Property(f => f.Name).HasColumnName("name").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).IsRequired();
    builder.Property(f => f.TaxonomyVersion).HasColumnName("taxonomy_version");
    builder.HasIndex(f => new { f.Key, f.TaxonomyVersion }).IsUnique();
  }
}

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
  public void Configure(EntityTypeBuilder<Topic> builder)
  {
    builder.ToTable("topics");
    builder.Property(t => t.FieldId).HasColumnName("field_id").IsRequired();
    builder.Property(t => t.Key).HasColumnName("key").HasMaxLength(80).IsRequired();
    builder.Property(t => t.Name).HasColumnName("name").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).IsRequired();
    builder.Property(t => t.TaxonomyVersion).HasColumnName("taxonomy_version");
    builder.HasIndex(t => new { t.Key, t.TaxonomyVersion }).IsUnique();
    builder.HasOne<Field>().WithMany(f => f.Topics).HasForeignKey(t => t.FieldId);
  }
}

public class EuroSciVocCrosswalkConfiguration : IEntityTypeConfiguration<EuroSciVocCrosswalk>
{
  public void Configure(EntityTypeBuilder<EuroSciVocCrosswalk> builder)
  {
    builder.ToTable("euro_sci_voc_crosswalk");
    builder.Property(c => c.EuroSciVocPathPrefix).HasColumnName("euro_sci_voc_path_prefix").HasMaxLength(500).IsRequired();
    builder.Property(c => c.TopicId).HasColumnName("topic_id").IsRequired();
    builder.Property(c => c.CrosswalkVersion).HasColumnName("crosswalk_version");
    builder.HasIndex(c => new { c.EuroSciVocPathPrefix, c.CrosswalkVersion }).IsUnique();
    builder.HasOne<Topic>().WithMany().HasForeignKey(c => c.TopicId);
  }
}
