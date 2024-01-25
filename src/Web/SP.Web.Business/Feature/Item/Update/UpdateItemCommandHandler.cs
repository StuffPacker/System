using System.Security.Authentication;
using MediatR;
using Sp.Api.Client.Feature.Feature.Item;
using SP.Shared.Common.Feature.Item.Dto;

namespace SP.Web.Business.Feature.Item.Update;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemViewModel>
{
    private readonly IApiItemClient _itemClient;

    public UpdateItemCommandHandler(IApiItemClient itemClient)
    {
        _itemClient = itemClient;
    }

    public async Task<ItemViewModel> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemClient.GetById(request.Id, request.UserId);
        if (item.UserId != request.UserId)
        {
            throw new AuthenticationException();
        }

        var dto = new ItemUpdateInputDto
        {
            Name = request.Model.Name,
            Weight = request.Model.Weight.ToString(),
            Description = request.Model.Description
        };
        var result = await _itemClient.Update(request.UserId, request.Id, dto);
        return new ItemViewModel(result);
    }
}