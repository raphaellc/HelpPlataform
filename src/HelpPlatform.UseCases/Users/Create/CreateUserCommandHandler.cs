using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.UserDomain;

namespace HelpPlatform.UseCases.Users.Create;

public class CreateUserCommandHandler(IRepository<User> repository) : ICommandHandler<CreateUserCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var newUser = new User(request.Name, request.Email);
    
    var createdUser = await repository.AddAsync(newUser, cancellationToken);

    return createdUser.Id;
  }
}
