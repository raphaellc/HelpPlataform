using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.UserDomain;

namespace HelpPlatform.UseCases.Users.List;

public class ListUsersHandler(IReadRepository<User> _repository) : IQueryHandler<ListUsersQuery, Result<IEnumerable<UserDto>>>
{
    public async Task<Result<IEnumerable<UserDto>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.ListAsync(cancellationToken);
        var userDtos = users.Select(user => new UserDto(user.Id, user.Name, user.Email));

        return Result.Success(userDtos);
    }
}
