using MediatR;

namespace SP.Web.Business.Feature.PackingList;

public class UpdateGroupItemsCommand : IRequest<string>
{
    public UpdateGroupItemsCommand(string id, string groupId, List<string> selectedItems, Guid user)
    {
        Id = id;
        GroupId = groupId;
        SelectedItems = selectedItems;
        User = user;
    }

    public List<string> SelectedItems { get; set; }

    public string GroupId { get; set; }

    public string Id { get; set; }

    public Guid User { get; set; }
}