using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Create;

public record CreateResourceTypeCommand(
  string Name,
  int Quantity,
  string Scale) : ICommand<Result<int>>;