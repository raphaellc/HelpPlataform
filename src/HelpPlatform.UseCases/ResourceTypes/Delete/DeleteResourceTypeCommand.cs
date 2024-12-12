using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Delete;

public record DeleteResourceTypeCommand(int ResourceTypeId) : ICommand<Result>;
