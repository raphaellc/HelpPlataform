using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.Users.Update;

// I don't believe we have reasons to add more mutable properties to the user entity.
public record UpdateUserCommand(int UserId, string NewName) : ICommand<Result<int>>;
