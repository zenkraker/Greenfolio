using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Greenfolio.API.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    org_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    method = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "collab_edges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    org_a = table.Column<int>(type: "integer", nullable: false),
                    org_b = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    first_year = table.Column<int>(type: "integer", nullable: false),
                    last_year = table.Column<int>(type: "integer", nullable: false),
                    project_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collab_edges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "collab_requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    from_name = table.Column<string>(type: "text", nullable: false),
                    from_email = table.Column<string>(type: "text", nullable: false),
                    from_company = table.Column<string>(type: "text", nullable: false),
                    target_org_id = table.Column<int>(type: "integer", nullable: true),
                    topic_id = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    consent_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collab_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    occurred_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    org_id = table.Column<int>(type: "integer", nullable: true),
                    topic_id = table.Column<int>(type: "integer", nullable: true),
                    query_text = table.Column<string>(type: "text", nullable: true),
                    meta = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    taxonomy_version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "org_merges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    kept_org_id = table.Column<int>(type: "integer", nullable: false),
                    merged_org_id = table.Column<int>(type: "integer", nullable: false),
                    method = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    confidence = table.Column<double>(type: "double precision", nullable: false),
                    decided_by = table.Column<string>(type: "text", nullable: true),
                    decided_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_merges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ror_id = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name_variants = table.Column<string[]>(type: "text[]", nullable: false),
                    country = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    org_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: true),
                    lng = table.Column<double>(type: "double precision", nullable: true),
                    website = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    search_vector = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true, computedColumnSql: "to_tsvector('english', coalesce(name, ''))", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orcid = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_id = table.Column<int>(type: "integer", nullable: false),
                    source_native_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    funding_amount = table.Column<decimal>(type: "numeric(14,2)", nullable: true),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    program = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    our_summary = table.Column<string>(type: "text", nullable: true),
                    source_url = table.Column<string>(type: "text", nullable: false),
                    search_vector = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true, computedColumnSql: "to_tsvector('english', coalesce(title, ''))", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publication_authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: true),
                    org_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publication_authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    openalex_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    doi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "scouting_leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    company = table.Column<string>(type: "text", nullable: false),
                    brief = table.Column<string>(type: "text", nullable: false),
                    budget_band = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    consent_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scouting_leads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    licence = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    attribution_required = table.Column<bool>(type: "boolean", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "suppression_list",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    matcher = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppression_list", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    org_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    taxonomy_version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_topics_fields_field_id",
                        column: x => x.field_id,
                        principalTable: "fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "org_source_records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    org_id = table.Column<int>(type: "integer", nullable: true),
                    source_id = table.Column<int>(type: "integer", nullable: false),
                    source_native_id = table.Column<string>(type: "text", nullable: false),
                    raw = table.Column<string>(type: "jsonb", nullable: false),
                    imported_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_source_records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_org_source_records_organizations_org_id",
                        column: x => x.org_id,
                        principalTable: "organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "org_topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    org_id = table.Column<int>(type: "integer", nullable: false),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    score = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_org_topics_organizations_org_id",
                        column: x => x.org_id,
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    org_id = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_project_participants_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    confidence = table.Column<double>(type: "double precision", nullable: false),
                    classifier_version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_project_topics_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "euro_sci_voc_crosswalk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    euro_sci_voc_path_prefix = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    topic_id = table.Column<int>(type: "integer", nullable: false),
                    crosswalk_version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_euro_sci_voc_crosswalk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_euro_sci_voc_crosswalk_topics_topic_id",
                        column: x => x.topic_id,
                        principalTable: "topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_collab_edges_org_a_org_b",
                table: "collab_edges",
                columns: new[] { "org_a", "org_b" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_euro_sci_voc_crosswalk_euro_sci_voc_path_prefix_crosswalk_v~",
                table: "euro_sci_voc_crosswalk",
                columns: new[] { "euro_sci_voc_path_prefix", "crosswalk_version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_euro_sci_voc_crosswalk_topic_id",
                table: "euro_sci_voc_crosswalk",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_occurred_at",
                table: "events",
                column: "occurred_at");

            migrationBuilder.CreateIndex(
                name: "IX_fields_key_taxonomy_version",
                table: "fields",
                columns: new[] { "key", "taxonomy_version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_org_merges_merged_org_id",
                table: "org_merges",
                column: "merged_org_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_org_source_records_org_id",
                table: "org_source_records",
                column: "org_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_source_records_source_id_source_native_id",
                table: "org_source_records",
                columns: new[] { "source_id", "source_native_id" });

            migrationBuilder.CreateIndex(
                name: "IX_org_topics_org_id_topic_id",
                table: "org_topics",
                columns: new[] { "org_id", "topic_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_organizations_ror_id",
                table: "organizations",
                column: "ror_id",
                unique: true,
                filter: "ror_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_organizations_search_vector",
                table: "organizations",
                column: "search_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_persons_orcid",
                table: "persons",
                column: "orcid",
                unique: true,
                filter: "orcid IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_project_participants_project_id_org_id_role",
                table: "project_participants",
                columns: new[] { "project_id", "org_id", "role" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_topics_project_id_topic_id",
                table: "project_topics",
                columns: new[] { "project_id", "topic_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_search_vector",
                table: "projects",
                column: "search_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_projects_source_id_source_native_id",
                table: "projects",
                columns: new[] { "source_id", "source_native_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publication_authors_publication_id_person_id_org_id",
                table: "publication_authors",
                columns: new[] { "publication_id", "person_id", "org_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publications_openalex_id",
                table: "publications",
                column: "openalex_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sources_key",
                table: "sources",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_suppression_list_matcher_value",
                table: "suppression_list",
                columns: new[] { "matcher", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_topics_field_id_key_taxonomy_version",
                table: "topics",
                columns: new[] { "field_id", "key", "taxonomy_version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claims");

            migrationBuilder.DropTable(
                name: "collab_edges");

            migrationBuilder.DropTable(
                name: "collab_requests");

            migrationBuilder.DropTable(
                name: "euro_sci_voc_crosswalk");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "org_merges");

            migrationBuilder.DropTable(
                name: "org_source_records");

            migrationBuilder.DropTable(
                name: "org_topics");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "project_participants");

            migrationBuilder.DropTable(
                name: "project_topics");

            migrationBuilder.DropTable(
                name: "publication_authors");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "scouting_leads");

            migrationBuilder.DropTable(
                name: "sources");

            migrationBuilder.DropTable(
                name: "suppression_list");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "topics");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "fields");
        }
    }
}
