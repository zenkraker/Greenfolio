using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// §3.4 classification, step 1: static, versioned, deterministic mapping from a CORDIS
// EuroSciVocPath prefix to one of our topics. LLM classification (step 2) only runs on
// what this crosswalk can't confidently resolve, plus non-CORDIS sources.
public class EuroSciVocCrosswalk(string euroSciVocPathPrefix, int topicId, int crosswalkVersion) : EntityBase, IAggregateRoot
{
  public string EuroSciVocPathPrefix { get; private set; } = Guard.Against.NullOrEmpty(euroSciVocPathPrefix, nameof(euroSciVocPathPrefix));
  public int TopicId { get; private set; } = topicId;
  public int CrosswalkVersion { get; private set; } = crosswalkVersion;
}
