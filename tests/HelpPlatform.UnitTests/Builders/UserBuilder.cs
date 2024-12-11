using HelpPlatform.Core.UserDomain;

namespace HelpPlatform.UnitTests.Builders;

public class UserBuilder
{
    private User? _user;

    public string TestName { get; private set; } = "test name";

    public string TestEmail { get; private set; } = "test email";

    public static UserBuilder WithDefaultValues()
    {
        return new UserBuilder();
    }

    public UserBuilder SetTestName(string name)
    {
        this.TestName = name;
        return this;
    }
    
    public UserBuilder SetTestEmail(string email)
    {
        this.TestEmail = email;
        return this;
    }

    public User Build()
    {
        this._user = new User(TestName, TestEmail);
        return this._user;
    }
}
