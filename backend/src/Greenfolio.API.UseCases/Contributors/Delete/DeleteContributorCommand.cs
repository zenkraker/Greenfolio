using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Greenfolio.API.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
