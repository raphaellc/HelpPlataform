using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.Users.Delete;

public record DeleteUserCommand(int UserId) : ICommand<Result>;
