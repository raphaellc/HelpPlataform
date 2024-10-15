using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Update;

// I don't believe we have reasons to add more mutable properties to the user entity.
public record UpdateResourceTypeCommand(int ResourceTypeId, string NewName, int NewQuantity, string NewScale) : ICommand<Result<int>>;