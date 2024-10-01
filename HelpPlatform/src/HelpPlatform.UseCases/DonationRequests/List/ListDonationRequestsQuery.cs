using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.List;

public record ListDonationRequestsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<DonationRequestDto>>>;
