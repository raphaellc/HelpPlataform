using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
