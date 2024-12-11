namespace HelpPlatform.UseCases.DonationRequests.ListClaimByRequest;

public interface IListClaimsByDonationRequestService
{
    Task<IEnumerable<DonationRequestClaimDto>> ListAsync(int? requestId = null, int? userId = null);
}
