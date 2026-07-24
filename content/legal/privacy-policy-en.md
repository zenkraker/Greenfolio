<!--
DRAFT — content only, not wired into the app yet (M0; shipping is M3 per docs/milestones/M0.md).
Legal template pending PO/lawyer sign-off (brief §8, open decision #6). [PLACEHOLDER] markers
need real values before publish. Written in English per the site's EN-primary decision
(brief §4.2) — confirm with lawyer whether a German-language version is also required
alongside the English one for German users/GDPR readability requirements.
-->

# Privacy Policy

Last updated: [DATE] · Version: [VERSION]

GreenGraph ("we", "us", the "Platform") maps public research activity in green technology —
organizations, projects, and their funding — to help companies find research partners. This
policy explains what personal data we process, where it comes from, why, and what rights you
have over it.

## 1. Who we are

**Data controller:** [LEGAL ENTITY NAME], [address] — see [Impressum](./impressum.md) for full
details.

**Contact for privacy matters:** [privacy contact email]

## 2. What personal data we process, and why

### 2.1 Researchers and project participants (Art. 6(1)(f) GDPR — legitimate interest)

We publish minimal profile information about people named as principal investigators,
authors, or project participants in public funding and publication records: **full name,
institutional affiliation, and links to the projects/publications where that name appears.**
No contact details, biography, or photo — this is deliberate (brief §3.2.2): person pages are
minimal by design.

**Source of this data (Art. 14 GDPR — we did not collect this from you directly):**

| Data element | Source | Already public? |
|---|---|---|
| Name, affiliation, project participation | CORDIS (EU-funded project records) | Yes — CORDIS publishes participant lists under an EU open-data licence |
| Name, affiliation, authorship | OpenAlex (scholarly metadata) | Yes — OpenAlex aggregates publicly available publication metadata under CC0 |
| Institutional identifiers used to link the above | ROR (Research Organization Registry) | Yes — CC0 public registry |

**Purpose:** to let companies discover who is researching a given topic at a given
organization, so they can reach out via our collaboration-request flow — the core function
of the Platform (brief §1).

**Legal basis:** legitimate interest (Art. 6(1)(f)). We've balanced this against your
interests as follows: the data was already public before we processed it, we show only what
the source already publishes (name, affiliation, project links — nothing invented, nothing
from private sources), and we do not sell this data or use it for advertising (brief §2,
anti-goals).

**Retention:** for as long as the source record remains valid, refreshed on each re-import;
removed on request (see §4) or when a source retracts the underlying record.

### 2.2 Company representatives (collaboration requests, scouting requests, account holders)

When you submit a collaboration request, request a scouting report, or create an account to
claim an organization profile, we process: name, email, company name, and whatever you write
in the request/brief field, plus consent-timestamp of submission.

**Legal basis:** Art. 6(1)(b) (performance of a contract / steps at your request) for account
and request handling; Art. 6(1)(a) (consent, recorded at submission) for the collaboration
request and scouting intake forms specifically.

**Retention:** for as long as your account is active, or until the request is closed plus
[retention period — TBD with lawyer] for record-keeping.

### 2.3 Analytics

We use self-hosted, cookieless analytics (Plausible/Umami — brief §3.2.6) that does not set
tracking cookies and does not build cross-session profiles. We log page views, search terms,
and filter/click events, associated with an aggregated first-party events record, not a
persistent identifier tied to you across visits.

**Legal basis:** legitimate interest (Art. 6(1)(f)) — measuring product usage without
tracking individuals.

**No cookie banner is shown** because no non-essential cookies are set (brief §3.2.6).

## 3. Who we share data with

We do not sell personal data. We share it only:

- With the organization a collaboration request targets (so they can respond to it).
- With our hosting provider [Hetzner or TBD], acting as a processor.
- Where legally required (e.g., a valid legal request).

## 4. Your rights

Under GDPR you have the right to:

- **Access** the personal data we hold about you.
- **Rectify** inaccurate data.
- **Object** to processing based on legitimate interest (Art. 21) — including a standing
  objection to appearing in our researcher/participant listings.
- **Erasure** ("right to be forgotten").
- **Restrict** processing.
- **Data portability**, where applicable.
- **Lodge a complaint** with a supervisory authority — [relevant DPA, e.g. the data
  protection authority for our jurisdiction, TBD once entity is formed].

### Removal and suppression (brief §3.2.2 / §6 EPIC E5)

Every organization and person page carries a **removal link**. Submitting it:

1. Removes the page from public view **within 72 hours**.
2. Adds you to our **suppression list**, matched by ROR ID, ORCID, or name — so re-importing
   the same source data later does not silently bring the page back.

To exercise any right in this section, contact [privacy contact email]. We aim to respond
within one month, as required by Art. 12(3) GDPR.

## 5. Data sources and licensing

Every record on the Platform carries its source, licence, and — where the source requires it
— attribution. The full list of sources and their licences is published on our
[Data Sources page](/datenquellen) (see brief §4.2; that page ships in M2). We do not
republish copyrighted text (e.g., applicant-written abstracts from Tier-2 sources) —
project/organization descriptions are either the source's own openly-licensed text or our
own original summary, always linked back to the original source (brief §3.2.3).

## 6. International transfers

[TBD with lawyer — depends on final hosting location and whether any processor is outside
the EEA.]

## 7. Changes to this policy

We'll post updates here with a new "last updated" date. Material changes affecting how we
process researcher data will be communicated via the same removal/suppression mechanism
described in §4.

---

### Open items before this can ship (M3)

1. Legal entity name and contact details — blocked on brand/domain decision (brief §8, item 1).
2. Confirm retention periods for collaboration/scouting request data with lawyer.
3. Confirm hosting provider and any sub-processors for §3 and §6.
4. Confirm supervisory authority for §4 once the entity's jurisdiction is settled.
5. Confirm whether a German-language version is required alongside this English draft.
6. Full lawyer sign-off per brief §8 item 6, specifically covering the Art. 14 module's
   legitimate-interest balancing test for researcher data (brief §3.2.2).
