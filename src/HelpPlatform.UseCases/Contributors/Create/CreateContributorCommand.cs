using Ardalis.Result;

namespace HelpPlatform.UseCases.Contributors.Create;

/// <summary>
/// Create a new Contributor.
/// </summary>
/// <param name="Name"></param>
public record CreateContributorCommand(string Name, string? PhoneNumber) : SharedKernel.ICommand<Result<int>>;
