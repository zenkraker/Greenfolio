#:package CsvHelper@33.1.0

// GreenGraph — Week 1 data spike.
//
// Answers three questions from CORDIS bulk data (Horizon Europe + H2020),
// using CORDIS's own EuroSciVoc classification as a green-field taxonomy,
// plus a live ROR API cross-check to estimate entity-resolution difficulty.
//
//   1. How many organizations and projects land in green fields?
//   2. What does battery/storage coverage look like per country?
//   3. How messy is entity matching really (sample orgs, ROR cross-check)?
//
// Run: dotnet run DataSpike.cs

using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using CsvHelper;
using CsvHelper.Configuration;

var dataDir = Path.Combine(AppContext.BaseDirectory, "..", "data");
dataDir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "data"));
var rawDir = Path.Combine(dataDir, "raw");
var outDir = Path.Combine(dataDir, "..", "output");
outDir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "output"));
Directory.CreateDirectory(outDir);

var extractedHorizon = Path.Combine(rawDir, "extracted");
var extractedH2020 = Path.Combine(rawDir, "extracted_h2020");

Console.WriteLine("=== GreenGraph Week 1 data spike ===");

// ---------------------------------------------------------------------
// 1. Load CORDIS CSVs (Horizon Europe + H2020)
// ---------------------------------------------------------------------

var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Delimiter = ";",
    BadDataFound = null,
    MissingFieldFound = null,
};

List<CordisOrg> LoadOrgs(string dir, string programme)
{
    var path = Path.Combine(dir, "organization.csv");
    using var reader = new StreamReader(path, Encoding.UTF8);
    using var csv = new CsvReader(reader, csvConfig);
    csv.Read();
    csv.ReadHeader();
    var list = new List<CordisOrg>();
    while (csv.Read())
    {
        list.Add(new CordisOrg(
            ProjectId: csv.GetField("projectID") ?? "",
            OrgId: csv.GetField("organisationID") ?? "",
            Name: (csv.GetField("name") ?? "").Trim(),
            ShortName: (csv.GetField("shortName") ?? "").Trim(),
            ActivityType: csv.GetField("activityType") ?? "",
            Country: (csv.GetField("country") ?? "").Trim().ToUpperInvariant(),
            City: csv.GetField("city") ?? "",
            Role: csv.GetField("role") ?? "",
            Programme: programme
        ));
    }
    return list;
}

List<CordisProject> LoadProjects(string dir, string programme)
{
    var path = Path.Combine(dir, "project.csv");
    using var reader = new StreamReader(path, Encoding.UTF8);
    using var csv = new CsvReader(reader, csvConfig);
    csv.Read();
    csv.ReadHeader();
    var list = new List<CordisProject>();
    while (csv.Read())
    {
        list.Add(new CordisProject(
            Id: csv.GetField("id") ?? "",
            Title: csv.GetField("title") ?? "",
            StartDate: csv.GetField("startDate") ?? "",
            Programme: programme
        ));
    }
    return list;
}

List<SciVocTag> LoadSciVoc(string dir)
{
    var path = Path.Combine(dir, "euroSciVoc.csv");
    using var reader = new StreamReader(path, Encoding.UTF8);
    using var csv = new CsvReader(reader, csvConfig);
    csv.Read();
    csv.ReadHeader();
    var list = new List<SciVocTag>();
    while (csv.Read())
    {
        list.Add(new SciVocTag(
            ProjectId: csv.GetField("projectID") ?? "",
            Path: csv.GetField("euroSciVocPath") ?? ""
        ));
    }
    return list;
}

Console.WriteLine("Loading CORDIS Horizon Europe...");
var orgsHorizon = LoadOrgs(extractedHorizon, "HORIZON");
var projectsHorizon = LoadProjects(extractedHorizon, "HORIZON");
var sciVocHorizon = LoadSciVoc(extractedHorizon);

Console.WriteLine("Loading CORDIS H2020...");
var orgsH2020 = LoadOrgs(extractedH2020, "H2020");
var projectsH2020 = LoadProjects(extractedH2020, "H2020");
var sciVocH2020 = LoadSciVoc(extractedH2020);

var allOrgRows = orgsHorizon.Concat(orgsH2020).ToList();
var allProjects = projectsHorizon.Concat(projectsH2020).ToDictionary(p => p.Id, p => p);
var allSciVoc = sciVocHorizon.Concat(sciVocH2020).ToList();

Console.WriteLine($"  org-participation rows: {allOrgRows.Count:N0}");
Console.WriteLine($"  distinct organisationID: {allOrgRows.Select(o => o.OrgId).Distinct().Count():N0}");
Console.WriteLine($"  projects: {allProjects.Count:N0}");
Console.WriteLine($"  euroSciVoc tag rows: {allSciVoc.Count:N0}");

// ---------------------------------------------------------------------
// 2. Green taxonomy — mapped onto CORDIS's own EuroSciVoc hierarchy.
//    First-pass proxy taxonomy for spike purposes only; the PO owns the
//    real v1 taxonomy per the project brief (M0).
// ---------------------------------------------------------------------

var greenFields = new Dictionary<string, string[]>
{
    ["Energy Storage & Batteries"] = new[]
    {
        "natural sciences/chemical sciences/electrochemistry/electric batteries",
        "engineering and technology/environmental engineering/energy and fuels/fuel cells",
    },
    ["Solar"] = new[]
    {
        "engineering and technology/environmental engineering/energy and fuels/renewable energy/solar energy",
    },
    ["Wind & Marine Energy"] = new[]
    {
        "engineering and technology/environmental engineering/energy and fuels/renewable energy/wind energy",
        "engineering and technology/environmental engineering/energy and fuels/renewable energy/hydroelectricity",
    },
    ["Hydrogen"] = new[]
    {
        "engineering and technology/environmental engineering/energy and fuels/renewable energy/hydrogen energy",
    },
    ["Bioenergy & Alternative Fuels"] = new[]
    {
        "engineering and technology/environmental engineering/energy and fuels/biomass energy",
        "engineering and technology/environmental engineering/energy and fuels/synthetic fuels",
        "engineering and technology/industrial biotechnology/biomaterials/biofuels",
        "agricultural sciences/agricultural biotechnology/biomass",
    },
    ["Geothermal"] = new[]
    {
        "engineering and technology/environmental engineering/energy and fuels/renewable energy/geothermal energy",
    },
    ["Circular Economy & Recycling"] = new[]
    {
        "engineering and technology/environmental engineering/waste management",
        "engineering and technology/environmental biotechnology/bioremediation",
    },
    ["Water Management"] = new[]
    {
        "engineering and technology/environmental engineering/water treatment processes",
        "engineering and technology/environmental engineering/natural resources management/water management",
    },
    ["Climate & Carbon Management"] = new[]
    {
        "engineering and technology/environmental engineering/ecosystem-based management",
        "engineering and technology/environmental engineering/carbon capture engineering",
        "natural sciences/earth and related environmental sciences/atmospheric sciences/climatology/climatic changes",
    },
    ["Sustainable Mobility"] = new[]
    {
        "social sciences/social geography/transport/electric vehicles",
        "social sciences/social geography/transport/sustainable transport",
    },
    ["Sustainable Buildings & Agriculture"] = new[]
    {
        "engineering and technology/civil engineering/architecture engineering/sustainable architecture",
        "agricultural sciences/agriculture, forestry, and fisheries/agriculture/sustainable agriculture",
        "agricultural sciences/agriculture, forestry, and fisheries/agriculture/horticulture/greenhouse horticulture",
    },
    ["Environmental Pollution & Governance"] = new[]
    {
        "engineering and technology/environmental engineering/air pollution engineering",
        "natural sciences/earth and related environmental sciences/environmental sciences/pollution",
        "natural sciences/earth and related environmental sciences/environmental sciences/sustainability sciences",
        "social sciences/law/environmental law",
        "social sciences/sociology/governance/environmental governance",
        "social sciences/economics and business/economics/sustainable economy",
    },
    ["Critical Materials (rare earths)"] = new[]
    {
        "engineering and technology/environmental engineering/mining and mineral processing/rare earths",
    },
};

// project id -> set of matched green fields
var projectFields = new Dictionary<string, HashSet<string>>();
foreach (var tag in allSciVoc)
{
    if (string.IsNullOrEmpty(tag.Path)) continue;
    foreach (var (field, prefixes) in greenFields)
    {
        if (prefixes.Any(p => tag.Path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
        {
            if (!projectFields.TryGetValue(tag.ProjectId, out var set))
            {
                set = new HashSet<string>();
                projectFields[tag.ProjectId] = set;
            }
            set.Add(field);
        }
    }
}

var greenProjectIds = projectFields.Keys.ToHashSet();
Console.WriteLine();
Console.WriteLine($"Green-tagged projects: {greenProjectIds.Count:N0} / {allProjects.Count:N0} ({100.0 * greenProjectIds.Count / allProjects.Count:F1}%)");

// orgs (distinct by OrgId) that participate in at least one green project
var orgIdToRows = allOrgRows.GroupBy(o => o.OrgId).ToDictionary(g => g.Key, g => g.ToList());
var greenOrgIds = allOrgRows.Where(o => greenProjectIds.Contains(o.ProjectId)).Select(o => o.OrgId).ToHashSet();
Console.WriteLine($"Distinct organizations touching a green project: {greenOrgIds.Count:N0}");
Console.WriteLine($"Distinct organizations overall: {orgIdToRows.Count:N0}");

// ---------------------------------------------------------------------
// 3. Per-field counts (Question 1)
// ---------------------------------------------------------------------

var fieldStats = new List<FieldStat>();
foreach (var field in greenFields.Keys)
{
    var projIds = projectFields.Where(kv => kv.Value.Contains(field)).Select(kv => kv.Key).ToHashSet();
    var orgIds = allOrgRows.Where(o => projIds.Contains(o.ProjectId)).Select(o => o.OrgId).ToHashSet();
    fieldStats.Add(new FieldStat(field, projIds.Count, orgIds.Count));
}
fieldStats = fieldStats.OrderByDescending(f => f.OrgCount).ToList();

Console.WriteLine();
Console.WriteLine("=== Question 1: counts per green field ===");
Console.WriteLine($"{"Field",-38} {"Projects",10} {"Orgs",10}");
foreach (var f in fieldStats)
    Console.WriteLine($"{f.Field,-38} {f.ProjectCount,10:N0} {f.OrgCount,10:N0}");

// ---------------------------------------------------------------------
// 4. Battery/storage wedge coverage per country (Question 2)
// ---------------------------------------------------------------------

var batteryFieldName = "Energy Storage & Batteries";
var batteryProjIds = projectFields.Where(kv => kv.Value.Contains(batteryFieldName)).Select(kv => kv.Key).ToHashSet();
// Widen the "wedge" slightly: batteries + hydrogen (fuel-cell/H2 storage is adjacent to the launch wedge per the brief).
var wedgeProjIds = projectFields
    .Where(kv => kv.Value.Contains("Energy Storage & Batteries") || kv.Value.Contains("Hydrogen"))
    .Select(kv => kv.Key).ToHashSet();

var wedgeOrgsRaw = allOrgRows.Where(o => wedgeProjIds.Contains(o.ProjectId)).ToList();
var wedgeByCountry = wedgeOrgsRaw
    .GroupBy(o => o.Country)
    .Select(g => new CountryStat(
        Country: g.Key,
        OrgCount: g.Select(o => o.OrgId).Distinct().Count(),
        ProjectCount: g.Select(o => o.ProjectId).Distinct().Count(),
        Universities: g.Where(o => o.ActivityType == "HES").Select(o => o.OrgId).Distinct().Count(),
        ResearchOrgs: g.Where(o => o.ActivityType == "REC").Select(o => o.OrgId).Distinct().Count(),
        Companies: g.Where(o => o.ActivityType is "PRC" or "SME").Select(o => o.OrgId).Distinct().Count()
    ))
    .OrderByDescending(c => c.OrgCount)
    .ToList();

Console.WriteLine();
Console.WriteLine($"=== Question 2: battery/storage + hydrogen wedge coverage per country ===");
Console.WriteLine($"Wedge projects: {wedgeProjIds.Count:N0}, distinct wedge orgs: {wedgeOrgsRaw.Select(o => o.OrgId).Distinct().Count():N0}");
Console.WriteLine($"{"Country",-10} {"Orgs",8} {"Projects",10} {"Univ",6} {"ResOrg",6} {"Company",8}");
foreach (var c in wedgeByCountry.Take(30))
    Console.WriteLine($"{c.Country,-10} {c.OrgCount,8:N0} {c.ProjectCount,10:N0} {c.Universities,6:N0} {c.ResearchOrgs,6:N0} {c.Companies,8:N0}");

// ---------------------------------------------------------------------
// 5a. Dedup check — intra-CORDIS name fragmentation (lower bound, no
//     external source needed): how often does the *same* normalized
//     name carry *different* organisationIDs?
// ---------------------------------------------------------------------

string NormalizeName(string name)
{
    var n = name.ToUpperInvariant();
    n = n.Normalize(NormalizationForm.FormD);
    var sb = new StringBuilder();
    foreach (var ch in n)
    {
        var cat = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch);
        if (cat != System.Globalization.UnicodeCategory.NonSpacingMark) sb.Append(ch);
    }
    n = sb.ToString().Normalize(NormalizationForm.FormC);
    foreach (var suffix in new[] { " GMBH", " LTD", " LIMITED", " S.R.O.", " SRO", " SP. Z O.O.", " SPZOO",
        " S.R.L.", " SRL", " S.L.", " SL", " S.A.", " SA", " SPA", " S.P.A.", " BV", " B.V.", " OY",
        " AB", " A/S", " AS", " AG", " NV", " N.V.", " KG", " E.V.", " EV", " INC", " CORP", " CORPORATION",
        " PLC", " GBR", " OU", " UAB", " ZRT", " KFT", " D.O.O.", " DOO" })
    {
        if (n.EndsWith(suffix)) n = n[..^suffix.Length];
    }
    n = new string(n.Where(c => char.IsLetterOrDigit(c) || c == ' ').ToArray());
    n = string.Join(' ', n.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    return n;
}

var distinctOrgs = orgIdToRows.Select(kv => new
{
    OrgId = kv.Key,
    Name = kv.Value[0].Name,
    Country = kv.Value.Select(r => r.Country).FirstOrDefault(c => !string.IsNullOrEmpty(c)) ?? "",
    ActivityType = kv.Value[0].ActivityType,
    IsGreen = greenOrgIds.Contains(kv.Key),
}).ToList();

var byNormName = distinctOrgs.GroupBy(o => (NormalizeName(o.Name), o.Country));
var fragmentedGroups = byNormName.Where(g => g.Select(o => o.OrgId).Distinct().Count() > 1).ToList();

Console.WriteLine();
Console.WriteLine("=== Question 3a: intra-CORDIS fragmentation (same normalized name+country, different organisationID) ===");
Console.WriteLine($"Distinct organisationIDs: {distinctOrgs.Count:N0}");
Console.WriteLine($"Distinct (normalized name, country) groups: {byNormName.Count():N0}");
Console.WriteLine($"Groups with >1 organisationID (likely same real org, split PIC): {fragmentedGroups.Count:N0} " +
    $"({100.0 * fragmentedGroups.Count / byNormName.Count():F2}% of name groups)");
Console.WriteLine("Sample fragmented groups:");
foreach (var g in fragmentedGroups.OrderByDescending(g => g.Count()).Take(15))
    Console.WriteLine($"  \"{g.Key.Item1}\" ({g.Key.Item2}): {g.Select(o => o.OrgId).Distinct().Count()} distinct organisationIDs, e.g. {string.Join(", ", g.Select(o => o.OrgId).Distinct().Take(4))}");

// ---------------------------------------------------------------------
// 5b. Dedup check — cross-source: sample 100 wedge orgs, query the live
//     ROR API, score name similarity, bucket by confidence.
// ---------------------------------------------------------------------

var wedgeDistinctOrgIds = wedgeOrgsRaw.Select(o => o.OrgId).Distinct().ToList();
var rng = new Random(42); // fixed seed for a reproducible sample
var sampleOrgIds = wedgeDistinctOrgIds.OrderBy(_ => rng.Next()).Take(100).ToList();
var sampleOrgs = sampleOrgIds.Select(id => distinctOrgs.First(o => o.OrgId == id)).ToList();

Console.WriteLine();
Console.WriteLine($"=== Question 3b: ROR cross-check on {sampleOrgs.Count} sampled wedge orgs ===");

using var http = new HttpClient();
http.Timeout = TimeSpan.FromSeconds(15);

double NameSimilarity(string a, string b)
{
    a = NormalizeName(a);
    b = NormalizeName(b);
    if (a == b) return 1.0;
    if (a.Length == 0 || b.Length == 0) return 0.0;
    // token Jaccard as a cheap, dependency-free proxy for fuzzy match quality
    var ta = a.Split(' ').ToHashSet();
    var tb = b.Split(' ').ToHashSet();
    var inter = ta.Intersect(tb).Count();
    var union = ta.Union(tb).Count();
    return union == 0 ? 0.0 : (double)inter / union;
}

var rorResults = new List<RorMatchResult>();
foreach (var org in sampleOrgs)
{
    try
    {
        var url = $"https://api.ror.org/v2/organizations?query={Uri.EscapeDataString(org.Name)}";
        var resp = await http.GetStringAsync(url);
        using var doc = JsonDocument.Parse(resp);
        var items = doc.RootElement.GetProperty("items");
        string bestName = "";
        string bestId = "";
        double bestScore = 0;
        foreach (var item in items.EnumerateArray().Take(5))
        {
            var id = item.GetProperty("id").GetString() ?? "";
            foreach (var nameObj in item.GetProperty("names").EnumerateArray())
            {
                var val = nameObj.GetProperty("value").GetString() ?? "";
                var sim = NameSimilarity(org.Name, val);
                if (sim > bestScore)
                {
                    bestScore = sim;
                    bestName = val;
                    bestId = id;
                }
            }
        }
        rorResults.Add(new RorMatchResult(org.Name, org.Country, org.ActivityType, bestName, bestId, bestScore, null));
    }
    catch (Exception ex)
    {
        rorResults.Add(new RorMatchResult(org.Name, org.Country, org.ActivityType, "", "", 0, ex.Message));
    }
    await Task.Delay(150); // be polite to the free public API
}

var high = rorResults.Count(r => r.Score >= 0.8);
var medium = rorResults.Count(r => r.Score is >= 0.5 and < 0.8);
var low = rorResults.Count(r => r.Score < 0.5);

Console.WriteLine($"High confidence (token-Jaccard >= 0.8): {high}");
Console.WriteLine($"Needs review (0.5 - 0.8):               {medium}");
Console.WriteLine($"Low / no confident match (< 0.5):       {low}");
Console.WriteLine();
Console.WriteLine("Breakdown by CORDIS activityType (HES=university, REC=research org, PRC/SME=company, PUB/OTH=other):");
foreach (var grp in rorResults.GroupBy(r => r.ActivityType).OrderByDescending(g => g.Count()))
{
    var h = grp.Count(r => r.Score >= 0.8);
    var m = grp.Count(r => r.Score is >= 0.5 and < 0.8);
    var l = grp.Count(r => r.Score < 0.5);
    Console.WriteLine($"  {grp.Key,-6} n={grp.Count(),3}  high={h,3}  medium={m,3}  low={l,3}");
}
Console.WriteLine();
Console.WriteLine($"{"CORDIS name",-42} {"Ctry",5} {"Type",5} {"Best ROR match",-42} {"Score",6}");
foreach (var r in rorResults.OrderBy(r => r.Score))
{
    var name = r.CordisName.Length > 41 ? r.CordisName[..41] : r.CordisName;
    var match = (r.BestRorName ?? "").Length > 41 ? r.BestRorName![..41] : r.BestRorName;
    Console.WriteLine($"{name,-42} {r.Country,5} {r.ActivityType,5} {match,-42} {r.Score,6:F2}");
}

// ---------------------------------------------------------------------
// 6. Write machine-readable outputs for the readout
// ---------------------------------------------------------------------

var summary = new JsonObject
{
    ["GeneratedAt"] = DateTime.UtcNow.ToString("o"),
    ["Totals"] = new JsonObject
    {
        ["ProjectsTotal"] = allProjects.Count,
        ["ProjectsGreen"] = greenProjectIds.Count,
        ["OrgRowsTotal"] = allOrgRows.Count,
        ["OrgsDistinctTotal"] = distinctOrgs.Count,
        ["OrgsDistinctGreen"] = greenOrgIds.Count,
    },
    ["FieldStats"] = new JsonArray(fieldStats.Select(f => (JsonNode)new JsonObject
    {
        ["Field"] = f.Field,
        ["ProjectCount"] = f.ProjectCount,
        ["OrgCount"] = f.OrgCount,
    }).ToArray()),
    ["WedgeCountryStats"] = new JsonArray(wedgeByCountry.Select(c => (JsonNode)new JsonObject
    {
        ["Country"] = c.Country,
        ["OrgCount"] = c.OrgCount,
        ["ProjectCount"] = c.ProjectCount,
        ["Universities"] = c.Universities,
        ["ResearchOrgs"] = c.ResearchOrgs,
        ["Companies"] = c.Companies,
    }).ToArray()),
    ["Dedup"] = new JsonObject
    {
        ["DistinctOrgIds"] = distinctOrgs.Count,
        ["DistinctNameCountryGroups"] = byNormName.Count(),
        ["FragmentedGroups"] = fragmentedGroups.Count,
        ["RorSampleSize"] = rorResults.Count,
        ["RorHighConfidence"] = high,
        ["RorNeedsReview"] = medium,
        ["RorLowConfidence"] = low,
    }
};

var jsonOpts = new JsonSerializerOptions { WriteIndented = true };
File.WriteAllText(Path.Combine(outDir, "summary.json"), summary.ToJsonString(jsonOpts));

var rorCsvPath = Path.Combine(outDir, "ror_sample.csv");
using (var writer = new StreamWriter(rorCsvPath))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(rorResults);
}

Console.WriteLine();
Console.WriteLine($"Wrote {Path.Combine(outDir, "summary.json")}");
Console.WriteLine($"Wrote {rorCsvPath}");
Console.WriteLine("=== Done ===");

// ---------------------------------------------------------------------
// Types
// ---------------------------------------------------------------------

record CordisOrg(string ProjectId, string OrgId, string Name, string ShortName, string ActivityType, string Country, string City, string Role, string Programme);
record CordisProject(string Id, string Title, string StartDate, string Programme);
record SciVocTag(string ProjectId, string Path);
record FieldStat(string Field, int ProjectCount, int OrgCount);
record CountryStat(string Country, int OrgCount, int ProjectCount, int Universities, int ResearchOrgs, int Companies);
record RorMatchResult(string CordisName, string Country, string ActivityType, string? BestRorName, string? BestRorId, double Score, string? Error);
