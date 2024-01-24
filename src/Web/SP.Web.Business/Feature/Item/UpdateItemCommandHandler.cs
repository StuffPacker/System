using MediatR;
using Sp.Api.Client.Feature.Feature.Item;
using SP.Shared.Common.Feature.Item.Dto;

namespace SP.Web.Business.Feature.Item;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemViewModel>
{
    private readonly IApiItemClient _itemClient;

    public UpdateItemCommandHandler(IApiItemClient itemClient)
    {
        _itemClient = itemClient;
    }

    public async Task<ItemViewModel> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var dto = new ItemEditInputDto
        {
            Name = request.Model.Name,
            Weight = request.Model.Weight
        };
        var result = await _itemClient.Update(request.UserId, request.Id, dto);
        return new ItemViewModel(result);
    }
}