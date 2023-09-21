using MediatR;
using SP.Database.Mongo.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList.GetPackingList;

public class GetPackingListCommandHandler : IRequestHandler<GetPackingListCommand, PackingListModel>
{
    private readonly IPackingListRepository _packingListRepository;

    public GetPackingListCommandHandler(IPackingListRepository packingListRepository)
    {
        _packingListRepository = packingListRepository;
    }

    public async Task<PackingListModel> Handle(GetPackingListCommand request, CancellationToken cancellationToken)
    {
        var model = await _packingListRepository.GetById(request.Id);
        return model;
    }
}