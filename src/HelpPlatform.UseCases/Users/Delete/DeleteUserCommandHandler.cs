using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.UserDomain.Interfaces;

namespace HelpPlatform.UseCases.Users.Delete;

public class DeleteUserCommandHandler(IDeleteUserService _deleteUserService) : ICommandHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _deleteUserService.DeleteUser(request.UserId, cancellationToken);
    }
}
