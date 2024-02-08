namespace SP.Shared.Common.Feature.User.Dto;

public class UserProfileDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public Guid UserId { get; set; }
}