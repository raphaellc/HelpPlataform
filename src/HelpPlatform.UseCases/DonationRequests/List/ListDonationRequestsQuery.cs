using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.List;

public record ListDonationRequestsQuery(int? Size, int? Index) : IQuery<Result<IEnumerable<DonationRequestDto>>>;
