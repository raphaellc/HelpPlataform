using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace HelpPlatform.Core.ResouceType;

public class ResouceType(string name, int quantity, string scale) : BaseEntity, IAggregateRoot
{
    
}