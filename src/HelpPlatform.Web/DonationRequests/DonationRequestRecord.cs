namespace HelpPlatform.Web.DonationRequests;

public record DonationRequestRecord(
    int id,
    string? description,
    DateTime deadline,
    string location,
    int resourceTypeId,
    int requestedQuantity,
    int fulfilledQuantity,
    string status,
    string? userName,
    DateTime createdAt);
