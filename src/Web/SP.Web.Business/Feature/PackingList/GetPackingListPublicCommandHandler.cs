using MediatR;
using Sp.Api.Client.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList;

public class GetPackingListPublicCommandHandler : IRequestHandler<GetPackingListPublicCommand, PackingListModel>
{
    private readonly IApiPackingListClient _apiPackingListClient;
    private readonly IPackingListMapper _packingListMapper;

    public GetPackingListPublicCommandHandler(IApiPackingListClient apiPackingListClient, IPackingListMapper packingListMapper)
    {
        _apiPackingListClient = apiPackingListClient;
        _packingListMapper = packingListMapper;
    }

    public async Task<PackingListModel> Handle(GetPackingListPublicCommand request, CancellationToken cancellationToken)
    {
        var model = await _apiPackingListClient.GetPackingListPublic(request.Id);
        return model;
    }
}