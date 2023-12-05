using MediatR;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList;

public class GetPackingListCommand : IRequest<PackingListModel>
{
    public GetPackingListCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; } = string.Empty;
}