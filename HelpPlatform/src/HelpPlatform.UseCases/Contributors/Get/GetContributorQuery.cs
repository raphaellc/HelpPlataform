using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
