using MediatR;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList.GetPackingList;

public class GetPackingListCommand : IRequest<PackingListModel>
{
    public GetPackingListCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}