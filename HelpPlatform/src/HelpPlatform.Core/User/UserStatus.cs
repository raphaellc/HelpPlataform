using Ardalis.SmartEnum;

namespace HelpPlatform.Core.User;

public class UserStatus : SmartEnum<UserStatus> {
    public static readonly UserStatus Requester = new(nameof(Requester), 0);

    public static readonly UserStatus Donor = new(nameof(Donor), 1);

    public static readonly UserStatus Volunteer = new(nameof(Volunteer), 2);

    public static readonly UserStatus Admin = new(nameof(Admin), 3);

    protected UserStatus(string name, int value) : base(name, value) {}
}