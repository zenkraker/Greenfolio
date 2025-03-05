using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Greenfolio.API.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
