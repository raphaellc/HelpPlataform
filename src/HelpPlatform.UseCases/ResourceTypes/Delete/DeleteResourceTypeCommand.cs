using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Delete;

public record DeleteResourceTypeCommand(int ResourceTypeId) : ICommand<Result>;