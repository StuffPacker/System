using MediatR;
using Sp.Api.Client.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList;

public class GetPackingListPrintCommandHandler : IRequestHandler<GetPackingListPrintCommand, PackingListModel>
{
    private readonly IApiPackingListClient _apiPackingListClient;
    private readonly IPackingListMapper _packingListMapper;

    public GetPackingListPrintCommandHandler(IApiPackingListClient apiPackingListClient, IPackingListMapper packingListMapper)
    {
        _apiPackingListClient = apiPackingListClient;
        _packingListMapper = packingListMapper;
    }

    public async Task<PackingListModel> Handle(GetPackingListPrintCommand request, CancellationToken cancellationToken)
    {
        var model = await _apiPackingListClient.GetPackingListPrint(request.Id);
        return model;
    }
}