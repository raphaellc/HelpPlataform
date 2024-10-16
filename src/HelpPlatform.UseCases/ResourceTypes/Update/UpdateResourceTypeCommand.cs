using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Update;

// I don't believe we have reasons to add more mutable properties to the user entity.
public record UpdateResourceTypeCommand(int ResourceTypeId, string NewName, string NewScale) : ICommand<Result<int>>;
