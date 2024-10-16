using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.ResourceTypeDomain;

namespace HelpPlatform.UseCases.ResourceTypes.Create;

public class CreateUserCommandHandler(IRepository<ResourceType> repository) : ICommandHandler<CreateResourceTypeCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateResourceTypeCommand request, CancellationToken cancellationToken)
  {
    var newResourceType = new ResourceType(request.Name, request.Scale);
    var createdResourceType = await repository.AddAsync(newResourceType, cancellationToken);

    return createdResourceType.Id;
  }
}
