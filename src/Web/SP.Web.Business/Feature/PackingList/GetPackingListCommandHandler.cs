using MediatR;
using Sp.Api.Client.Feature.Health;
using Sp.Api.Client.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList;

public class GetPackingListCommandHandler : IRequestHandler<GetPackingListCommand, PackingListModel?>
{
    private readonly IApiPackingListClient _apiPackingListClient;

    public GetPackingListCommandHandler(IApiPackingListClient apiPackingListClient)
    {
        _apiPackingListClient = apiPackingListClient;
    }

    public async Task<PackingListModel?> Handle(GetPackingListCommand request, CancellationToken cancellationToken)
    {
        var model = await _apiPackingListClient.GetPackingList(request.Id, request.CurrentUser);
        return model;
    }
}