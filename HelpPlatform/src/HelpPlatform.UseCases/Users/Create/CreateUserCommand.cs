using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.Users.Create;

public record CreateUserCommand(
  string Name,
  string Email) : ICommand<Result<int>>;
