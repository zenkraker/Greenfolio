using Greenfolio.API.Core.GreenGraph;

namespace Greenfolio.API.Infrastructure.Data;

/// <summary>
/// Taxonomy v1 draft (brief §3.4) — PO-owned, pending review (open decision #3, brief §8).
/// Field/topic set below adapts the field list validated in the Week 1 data spike, expanded
/// to topic level. Crosswalk only covers what CORDIS's own EuroSciVoc vocabulary can actually
/// distinguish; finer topics (e.g. "solid-state batteries" vs "flow batteries") have no
/// crosswalk row on purpose — those are LLM-classification-only (§3.4 step 2) until EuroSciVoc
/// itself gets more granular or a future crosswalk version adds heuristics for them.
/// </summary>
public static class TaxonomySeed
{
  public const int TaxonomyVersion = 1;
  public const int CrosswalkVersion = 1;

  private static readonly (string FieldKey, string FieldName, (string TopicKey, string TopicName)[] Topics)[] Taxonomy =
  [
    ("energy-storage", "Energy Storage & Batteries", [
      ("general", "Energy storage (general)"),
      ("solid-state-batteries", "Solid-state batteries"),
      ("lithium-ion-batteries", "Lithium-ion batteries"),
      ("flow-batteries", "Flow batteries"),
      ("battery-recycling", "Battery recycling & second life"),
      ("battery-management-systems", "Battery management systems (BMS)"),
      ("fuel-cells", "Fuel cells"),
    ]),
    ("solar", "Solar", [
      ("general", "Solar (general)"),
      ("photovoltaic", "Photovoltaic"),
      ("solar-thermal", "Solar thermal"),
      ("concentrated-solar-power", "Concentrated solar power"),
      ("perovskite-next-gen-pv", "Perovskite & next-gen PV"),
    ]),
    ("wind-marine", "Wind & Marine Energy", [
      ("general", "Wind & marine energy (general)"),
      ("wind-energy", "Wind energy"),
      ("hydropower", "Hydropower"),
      ("marine-energy", "Marine energy (tidal & wave)"),
    ]),
    ("hydrogen", "Hydrogen", [
      ("general", "Hydrogen (general)"),
      ("green-hydrogen-production", "Green hydrogen production"),
      ("hydrogen-storage", "Hydrogen storage"),
      ("hydrogen-infrastructure", "Hydrogen transport & infrastructure"),
      ("fuel-cell-vehicles", "Fuel cell vehicles"),
    ]),
    ("bioenergy", "Bioenergy & Alternative Fuels", [
      ("general", "Bioenergy (general)"),
      ("biomass-energy", "Biomass energy"),
      ("biofuels", "Biofuels"),
      ("synthetic-e-fuels", "Synthetic / e-fuels"),
      ("biogas-anaerobic-digestion", "Biogas & anaerobic digestion"),
    ]),
    ("geothermal", "Geothermal", [
      ("general", "Geothermal (general)"),
      ("deep-geothermal", "Deep geothermal"),
      ("ground-source-heat-pumps", "Ground-source heat pumps"),
    ]),
    ("circular-economy", "Circular Economy & Recycling", [
      ("general", "Circular economy (general)"),
      ("waste-treatment-recovery", "Waste treatment & recovery"),
      ("recycling-technologies", "Recycling technologies"),
      ("remanufacturing", "Remanufacturing"),
      ("bioremediation", "Bioremediation"),
      ("composting", "Composting"),
    ]),
    ("water", "Water Management", [
      ("general", "Water management (general)"),
      ("drinking-water-treatment", "Drinking water treatment"),
      ("wastewater-treatment", "Wastewater treatment"),
      ("water-resource-management", "Water resource management"),
    ]),
    ("climate-carbon", "Climate & Carbon Management", [
      ("general", "Climate & carbon management (general)"),
      ("carbon-capture-storage", "Carbon capture & storage"),
      ("climate-change-mitigation", "Climate change mitigation"),
      ("climate-change-adaptation", "Climate change adaptation"),
      ("nature-based-solutions", "Nature-based solutions"),
      ("ecosystem-restoration", "Ecosystem restoration"),
    ]),
    ("mobility", "Sustainable Mobility", [
      ("general", "Sustainable mobility (general)"),
      ("electric-vehicles", "Electric vehicles"),
      ("sustainable-transport-systems", "Sustainable transport systems"),
      ("intelligent-transport-systems", "Intelligent transport systems"),
    ]),
    ("buildings-agri", "Sustainable Buildings & Agriculture", [
      ("general", "Sustainable buildings & agriculture (general)"),
      ("sustainable-architecture", "Sustainable architecture"),
      ("sustainable-building-materials", "Sustainable building materials"),
      ("sustainable-agriculture", "Sustainable agriculture"),
      ("greenhouse-horticulture", "Greenhouse horticulture"),
    ]),
    ("environment-governance", "Environmental Pollution & Governance", [
      ("general", "Environment & governance (general)"),
      ("air-pollution-control", "Air pollution control"),
      ("environmental-law-policy", "Environmental law & policy"),
      ("environmental-governance", "Environmental governance"),
      ("sustainability-economics", "Sustainability economics"),
    ]),
    ("critical-materials", "Critical Materials", [
      ("general", "Critical materials (general)"),
      ("rare-earth-elements", "Rare earth elements"),
      ("battery-raw-materials", "Battery raw materials"),
    ]),
  ];

  // EuroSciVocPath prefix -> (fieldKey, topicKey). Only entries CORDIS's own vocabulary can
  // actually resolve deterministically; see class remarks.
  private static readonly (string PathPrefix, string FieldKey, string TopicKey)[] Crosswalk =
  [
    ("natural sciences/chemical sciences/electrochemistry/electric batteries", "energy-storage", "general"),
    ("engineering and technology/environmental engineering/energy and fuels/fuel cells", "energy-storage", "fuel-cells"),

    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/solar energy", "solar", "general"),
    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/solar energy/photovoltaic", "solar", "photovoltaic"),
    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/solar energy/solar thermal", "solar", "solar-thermal"),
    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/solar energy/concentrated solar power", "solar", "concentrated-solar-power"),

    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/wind energy", "wind-marine", "wind-energy"),
    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/hydroelectricity", "wind-marine", "hydropower"),
    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/hydroelectricity/marine energy", "wind-marine", "marine-energy"),

    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/hydrogen energy", "hydrogen", "general"),

    ("engineering and technology/environmental engineering/energy and fuels/biomass energy", "bioenergy", "biomass-energy"),
    ("engineering and technology/environmental engineering/energy and fuels/synthetic fuels", "bioenergy", "synthetic-e-fuels"),
    ("engineering and technology/industrial biotechnology/biomaterials/biofuels", "bioenergy", "biofuels"),
    ("agricultural sciences/agricultural biotechnology/biomass", "bioenergy", "biomass-energy"),

    ("engineering and technology/environmental engineering/energy and fuels/renewable energy/geothermal energy", "geothermal", "general"),

    ("engineering and technology/environmental engineering/waste management", "circular-economy", "general"),
    ("engineering and technology/environmental engineering/waste management/waste treatment processes", "circular-economy", "waste-treatment-recovery"),
    ("engineering and technology/environmental engineering/waste management/waste treatment processes/recycling", "circular-economy", "recycling-technologies"),
    ("engineering and technology/environmental engineering/waste management/waste treatment processes/remanufacturing", "circular-economy", "remanufacturing"),
    ("engineering and technology/environmental biotechnology/bioremediation", "circular-economy", "bioremediation"),
    ("engineering and technology/environmental biotechnology/bioremediation/compost", "circular-economy", "composting"),

    ("engineering and technology/environmental engineering/water treatment processes", "water", "general"),
    ("engineering and technology/environmental engineering/water treatment processes/drinking water treatment processes", "water", "drinking-water-treatment"),
    ("engineering and technology/environmental engineering/water treatment processes/wastewater treatment processes", "water", "wastewater-treatment"),
    ("engineering and technology/environmental engineering/natural resources management/water management", "water", "water-resource-management"),

    ("engineering and technology/environmental engineering/carbon capture engineering", "climate-carbon", "carbon-capture-storage"),
    ("engineering and technology/environmental engineering/ecosystem-based management", "climate-carbon", "general"),
    ("engineering and technology/environmental engineering/ecosystem-based management/climatic change mitigation", "climate-carbon", "climate-change-mitigation"),
    ("engineering and technology/environmental engineering/ecosystem-based management/climate change adaptation", "climate-carbon", "climate-change-adaptation"),
    ("engineering and technology/environmental engineering/ecosystem-based management/nature-based solutions", "climate-carbon", "nature-based-solutions"),
    ("engineering and technology/environmental engineering/ecosystem-based management/ecological restoration", "climate-carbon", "ecosystem-restoration"),
    ("natural sciences/earth and related environmental sciences/atmospheric sciences/climatology/climatic changes", "climate-carbon", "general"),

    ("social sciences/social geography/transport/electric vehicles", "mobility", "electric-vehicles"),
    ("social sciences/social geography/transport/sustainable transport", "mobility", "sustainable-transport-systems"),
    ("social sciences/social geography/transport/sustainable transport/intelligent transport systems", "mobility", "intelligent-transport-systems"),

    ("engineering and technology/civil engineering/architecture engineering/sustainable architecture", "buildings-agri", "sustainable-architecture"),
    ("engineering and technology/civil engineering/architecture engineering/sustainable architecture/sustainable building", "buildings-agri", "sustainable-building-materials"),
    ("agricultural sciences/agriculture, forestry, and fisheries/agriculture/sustainable agriculture", "buildings-agri", "sustainable-agriculture"),
    ("agricultural sciences/agriculture, forestry, and fisheries/agriculture/horticulture/greenhouse horticulture", "buildings-agri", "greenhouse-horticulture"),

    ("engineering and technology/environmental engineering/air pollution engineering", "environment-governance", "air-pollution-control"),
    ("natural sciences/earth and related environmental sciences/environmental sciences/pollution", "environment-governance", "air-pollution-control"),
    ("natural sciences/earth and related environmental sciences/environmental sciences/sustainability sciences", "environment-governance", "sustainability-economics"),
    ("social sciences/law/environmental law", "environment-governance", "environmental-law-policy"),
    ("social sciences/sociology/governance/environmental governance", "environment-governance", "environmental-governance"),
    ("social sciences/economics and business/economics/sustainable economy", "environment-governance", "sustainability-economics"),

    ("engineering and technology/environmental engineering/mining and mineral processing/rare earths", "critical-materials", "rare-earth-elements"),
  ];

  public static void Seed(AppDbContext dbContext)
  {
    if (dbContext.Fields.Any()) return;

    var fieldsByKey = new Dictionary<string, Field>();
    var topicsByKey = new Dictionary<(string FieldKey, string TopicKey), Topic>();

    foreach (var (fieldKey, fieldName, topics) in Taxonomy)
    {
      var field = new Field(fieldKey, fieldName, TaxonomyVersion);
      fieldsByKey[fieldKey] = field;
      dbContext.Fields.Add(field);
    }
    dbContext.SaveChanges(); // assigns field Ids

    foreach (var (fieldKey, _, topics) in Taxonomy)
    {
      var field = fieldsByKey[fieldKey];
      foreach (var (topicKey, topicName) in topics)
      {
        var topic = new Topic(field.Id, topicKey, topicName, TaxonomyVersion);
        topicsByKey[(fieldKey, topicKey)] = topic;
        dbContext.Topics.Add(topic);
      }
    }
    dbContext.SaveChanges(); // assigns topic Ids

    foreach (var (pathPrefix, fieldKey, topicKey) in Crosswalk)
    {
      var topic = topicsByKey[(fieldKey, topicKey)];
      dbContext.EuroSciVocCrosswalks.Add(new EuroSciVocCrosswalk(pathPrefix, topic.Id, CrosswalkVersion));
    }
    dbContext.SaveChanges();
  }
}
