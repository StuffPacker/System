using MediatR;
using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList;

public class GetPackingListPublicCommand : IRequest<PackingListModel>
{
    public GetPackingListPublicCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; } = string.Empty;
}