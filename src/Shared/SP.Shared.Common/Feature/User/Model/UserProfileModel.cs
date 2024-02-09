namespace SP.Shared.Common.Feature.User.Model;

public class UserProfileModel
{
    public string Id { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
}