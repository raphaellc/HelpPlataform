using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.User.Interfaces;

namespace HelpPlatform.UseCases.Users.Delete;

public class DeleteUserCommandHandler(IDeleteUserService _deleteUserService) : ICommandHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _deleteUserService.DeleteUser(request.UserId, cancellationToken);
    }
}
