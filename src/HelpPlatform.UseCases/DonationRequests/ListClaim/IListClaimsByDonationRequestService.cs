namespace HelpPlatform.UseCases.DonationRequests.ListClaim;

public interface IListClaimsByDonationRequestService
{
    Task<IEnumerable<DonationRequestClaimDto>> ListAsync(int requestId);
}
