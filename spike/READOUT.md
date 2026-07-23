# GreenGraph — Week 1 Data Spike Readout

**Date:** 2026-07-23 · **Sources:** CORDIS bulk CSVs (Horizon Europe 2021–27 + H2020 2014–20), live ROR API · **Tool:** `spike/DataSpike.cs` (.NET 10 file-based C# script, one evening)

## Verdict: **green light — proceed with confidence**, with two scope adjustments (see bottom)

The thesis holds up on real data. CORDIS alone — before OpenAlex, ROR-backed entity resolution, or the other Tier-1 sources — already lands in the neighborhood of the full-platform Week-16 targets, and the wedge is not thin. The dirtiest part isn't what the brief expects (university dedup): it's clean. The real gap is company-side identity, which has no registry to lean on at all.

## Q1 — How many orgs/projects land in green fields?

CORDIS's own **EuroSciVoc** classification (a hierarchical taxonomy it already tags every project with) was used as a proxy for the green taxonomy — no LLM classifier needed for this spike.

| | Count |
|---|---:|
| Total CORDIS projects (HORIZON + H2020) | 58,667 |
| Green-tagged projects | 8,335 (14.2%) |
| Total distinct organizations (by PIC/organisationID) | 62,196 |
| Distinct organizations touching ≥1 green project | 23,624 |

**By field** (projects / orgs, sorted by org count):

| Field | Projects | Orgs |
|---|---:|---:|
| Environmental Pollution & Governance | 2,434 | 10,909 |
| Climate & Carbon Management | 2,796 | 10,540 |
| Circular Economy & Recycling | 1,322 | 5,646 |
| Water Management | 684 | 3,023 |
| Sustainable Buildings & Agriculture | 444 | 2,876 |
| Solar | 682 | 2,088 |
| Wind & Marine Energy | 528 | 2,053 |
| Sustainable Mobility | 343 | 2,013 |
| **Energy Storage & Batteries** | 489 | 1,967 |
| Bioenergy & Alternative Fuels | 372 | 1,682 |
| Hydrogen | 185 | 1,035 |
| Geothermal | 80 | 525 |
| Critical Materials (rare earths) | 3 | 47 |

*Caveat: this is a first-pass proxy mapping onto EuroSciVoc paths, not the PO's real v1 taxonomy — field boundaries will shift, but the scale and relative ranking are a solid signal.* One early warning: **rare-earths/critical-materials coverage is thin** (47 orgs) — if the battery narrative wants a supply-chain angle, this needs broadening or a different source.

## Q2 — Battery/storage coverage per country (wedge = Energy Storage & Batteries + Hydrogen)

629 projects, **2,530 distinct organizations** — already well past the MVP target of 1,500 wedge orgs, from CORDIS alone.

| Country | Orgs | Projects | Univ | Res.org | Company |
|---|---:|---:|---:|---:|---:|
| DE | 331 | 291 | 40 | 27 | 246 |
| FR | 296 | 228 | 35 | 17 | 224 |
| IT | 248 | 220 | 33 | 22 | 185 |
| ES | 243 | 219 | 26 | 45 | 150 |
| UK | 215 | 199 | 53 | 5 | 134 |
| NL | 152 | 141 | 9 | 9 | 126 |
| BE | 120 | 126 | 7 | 14 | 76 |
| SE | 96 | 85 | 10 | 7 | 66 |
| AT | 81 | 85 | 6 | 11 | 59 |
| NO | 74 | 81 | 5 | 7 | 53 |

Coverage is broad, not concentrated in one or two countries — good for a pan-European narrative. Germany/France/Italy/Spain/UK form the core five, consistent with overall EU R&D funding volume. Long tail continues through PL, CZ, SI, EE, HR, IE, RO, and non-EU associated countries (NO, CH, IL, TR, UA) — the "pan-European, not just EU" framing works.

## Q3 — How messy is entity matching, really?

Two tests, from clean to messy:

**1. Intra-CORDIS fragmentation** (same normalized name+country carrying >1 distinct organisationID/PIC) — **0.26%** (164 of 62,028 name-groups). CORDIS's own participant registry is not messy internally. This is a lower bound only; it doesn't test cross-source matching.

**2. Cross-source: 100 sampled wedge orgs, queried live against ROR, hand-checked:**

| Org type (n) | High conf. | Needs review | No match |
|---|---:|---:|---:|
| Universities + research orgs (30) | 14 | 13 | 3 |
| Companies (66) | 5 | 15 | 46 |

Hand-checking the "needs review" and "no match" rows changes the story:

- **Most university "needs review" cases are actually correct matches**, just under-scored by a naive similarity metric — e.g. *Universidad de Lleida* ↔ *Universidad de Lérida* (Catalan/Spanish name variant), *Westfälische Hochschule Gelsenkirchen…* ↔ *Westfälische Hochschule* (legal long-form vs. short form). True resolvability on the university/institute side is closer to **~90%+** with the layered matcher the brief already specs (normalize → alias list → fuzzy), not the raw ~47% the crude score suggests.
- **One genuine false-positive surfaced**, worth remembering during M1 build: *Technische Universität Chemnitz* was matched to *TU Munich* at medium confidence — the correct ROR entity, *Chemnitz University of Technology*, exists but wasn't retrieved by my simple top-5 query. Confirms the brief's instinct that "Technische Universität X" collisions across Germany need location cross-checks, not name similarity alone.
- **Companies are a different problem entirely, not a harder version of the same one.** Hand-verified two "no match" cases directly against the ROR API (INERATEC GmbH, Talga AB — both real, known battery/hydrogen firms) — zero ROR records exist for either. **ROR doesn't track ordinary companies by design; it's a research-organization registry.** 70% of the company sample failing isn't a matching-algorithm problem to tune away — there's no registry backbone for that half of the graph at all.

## What this means for the plan

**Proceed to M0/M1 as scoped**, with two adjustments the brief doesn't currently cover:

1. **Company-side entity resolution needs its own strategy**, separate from the ROR-backbone design in §3.3. ROR only backs the university/institute half of the graph. Company identity will have to run on name+VAT/domain normalization plus the claim flow for verification — budget for it explicitly rather than assuming the ROR pipeline extends there.
2. **Critical-materials/rare-earths coverage is thin from CORDIS alone** (47 orgs) — fine to launch without it, but don't lean on a supply-chain-depth story for the wedge without a supplementary source.

Neither is a red flag on the core thesis — both are scoping notes for M1, not blockers.

---

### Reproduce this

```bash
sudo apt-get install -y dotnet-sdk-10.0   # via packages.microsoft.com apt repo
cd spike
dotnet run DataSpike.cs
```

Raw CORDIS zips are cached in `spike/data/raw/` (not committed — see `.gitignore`). Machine-readable output: `spike/output/summary.json`, `spike/output/ror_sample.csv`.
