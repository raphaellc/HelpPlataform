using Ardalis.Result;
using HelpPlatform.Core.UserDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.UserDomain.Services;

public class UpdateUserService(IRepository<User> _repository) : IUpdateUserService
{
    public async Task<Result> UpdateUser(int userId, string name, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByIdAsync(userId, cancellationToken);
        if (existingUser is null)
        {
            return Result.NotFound();
        }

        existingUser.UpdateName(name);
        
        await _repository.UpdateAsync(existingUser, cancellationToken);

        return Result.NoContent(); //Result.Success()?
    }
}
