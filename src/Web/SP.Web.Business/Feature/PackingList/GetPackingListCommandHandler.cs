using MediatR;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Business.Feature.PackingList;

public class GetPackingListCommandHandler : IRequestHandler<GetPackingListCommand, PackingListModel>
{
    public async Task<PackingListModel> Handle(GetPackingListCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}