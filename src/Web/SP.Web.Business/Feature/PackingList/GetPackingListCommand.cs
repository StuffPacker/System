using MediatR;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList;

public class GetPackingListCommand : IRequest<PackingListModel>
{
    public GetPackingListCommand(string id, Guid currentUser)
    {
        Id = id;
        CurrentUser = currentUser;
    }

    public string Id { get; set; } = string.Empty;

    public Guid CurrentUser { get; set; }
}