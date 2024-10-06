using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.ResourceType;

namespace HelpPlatform.UseCases.ResourceTypes.Create;

public class CreateUserCommandHandler(IRepository<ResourceType> repository) : ICommandHandler<CreateResourceTypeCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateResourceTypeCommand request, CancellationToken cancellationToken)
  {
    var newResourceType = new ResourceType(request.Name, request.Quantity, request.Scale);
    var createdResourceType = await repository.AddAsync(newResourceType, cancellationToken);

    return createdResourceType.Id;
  }
}