using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Create;

public record CreateResourceTypeCommand(
  string Name,
  string Scale) : ICommand<Result<int>>;
