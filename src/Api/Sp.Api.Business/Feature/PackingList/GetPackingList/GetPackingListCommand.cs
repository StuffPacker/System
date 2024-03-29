using MediatR;
using SP.Shared.Common.Feature.PackingList.Model;

namespace Sp.Api.Business.Feature.PackingList.GetPackingList;

public class GetPackingListCommand : IRequest<PackingListModel>
{
    public GetPackingListCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}