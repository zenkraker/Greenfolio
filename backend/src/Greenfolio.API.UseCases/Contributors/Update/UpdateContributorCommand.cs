using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Greenfolio.API.UseCases.Contributors.Update;

public record UpdateContributorCommand(int ContributorId, string NewName) : ICommand<Result<ContributorDTO>>;
