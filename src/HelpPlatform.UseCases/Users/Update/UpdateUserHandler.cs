using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.UserDomain.Interfaces;

namespace HelpPlatform.UseCases.Users.Update;

public class UpdateUserHandler(IUpdateUserService _updateUserService) : ICommandHandler<UpdateUserCommand, Result<int>>
{
    public async Task<Result<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await _updateUserService.UpdateUser(request.UserId, request.NewName, cancellationToken);
    }
}
