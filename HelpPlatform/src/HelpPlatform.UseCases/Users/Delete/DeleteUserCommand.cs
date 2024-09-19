using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.Users.Delete;

public record DeleteUserCommand(int UserId) : ICommand<Result>;
