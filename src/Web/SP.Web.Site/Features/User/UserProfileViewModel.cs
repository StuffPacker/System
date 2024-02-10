using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SP.Shared.Common.Feature.User.Model;

namespace SP.Web.Site.Features.User;

public class UserProfileViewModel
{
    public UserProfileViewModel(UserProfileModel result)
    {
        Id = result.UserId.ToString();
        DatabaseId = result.Id;
        Name = result.Name;
    }

    public string Name { get; set; }

    public string DatabaseId { get; set; }

    public string Id { get; set; }

    public bool IsOwner { get; set; }
}