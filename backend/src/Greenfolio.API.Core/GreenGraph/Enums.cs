namespace Greenfolio.API.Core.GreenGraph;

public enum OrgType
{
  University,
  Institute,
  Company,
  Other
}

public enum OrgStatus
{
  Auto,
  Claimed,
  Removed,
  Suppressed
}

public enum PersonStatus
{
  Auto,
  Removed,
  Suppressed
}

public enum ParticipantRole
{
  Coordinator,
  Partner
}

public enum ClaimMethod
{
  EmailDomain,
  Manual
}

public enum ClaimStatus
{
  Pending,
  Approved,
  Rejected
}

public enum UserRole
{
  Owner,
  Admin
}

public enum CollabRequestStatus
{
  New,
  Forwarded,
  Matched,
  Closed
}

public enum ScoutingLeadStatus
{
  New,
  Contacted,
  Delivered,
  Closed
}

public enum EventType
{
  Search,
  Filter,
  OrgView,
  GraphView,
  CollabRequest,
  ScoutingLead
}

public enum SuppressionMatcher
{
  RorId,
  Orcid,
  Name
}

public enum OrgMergeMethod
{
  ExactId,
  NormalizedName,
  Fuzzy,
  LlmAdjudication,
  Manual
}
