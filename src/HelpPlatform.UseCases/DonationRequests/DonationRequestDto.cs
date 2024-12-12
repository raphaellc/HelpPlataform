namespace HelpPlatform.UseCases.DonationRequests;

public class DonationRequestDto(int id, string? description, DateTime deadline, string location, int resourceTypeId, int requestedQuantity, int fulfilledQuantity, string status, string? userName, DateTime createdAt)
{
    public int Id { get; set; } = id;

    public string? Description { get; set; } = description;

    public DateTime Deadline { get; set; } = deadline;

    public string Location { get; set; } = location;

    public int ResourceTypeId { get; set; } = resourceTypeId;

    public int RequestedQuantity { get; set; } = requestedQuantity;

    public int FulfilledQuantity { get; set; } = fulfilledQuantity;

    public string Status { get; set; } = status;

    public string? UserName { get; set; } = userName;

    public DateTime CreatedAt { get; set; } = createdAt;
}
