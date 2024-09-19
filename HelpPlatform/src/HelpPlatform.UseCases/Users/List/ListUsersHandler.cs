using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.User;

namespace HelpPlatform.UseCases.Users.List;

public class ListUsersHandler(IReadRepository<User> _repository) : IQueryHandler<ListUsersQuery, Result<IEnumerable<UserDTO>>>
{
    public async Task<Result<IEnumerable<UserDTO>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.ListAsync(cancellationToken);
        var userDtos = users.Select(user => new UserDTO(user.Id, user.Name, user.Email));

        return Result.Success(userDtos);
    }
}
