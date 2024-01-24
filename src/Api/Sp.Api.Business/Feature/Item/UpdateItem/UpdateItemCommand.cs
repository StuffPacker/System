using MediatR;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.UpdateItem;

public class UpdateItemCommand : IRequest<ItemDto>
{
    public UpdateItemCommand(string id, Guid userId, ItemEditInputDto inputModel)
    {
        Id = id;
        UserId = userId;
        InputModel = inputModel;
    }

    public ItemEditInputDto InputModel { get; set; }

    public Guid UserId { get; set; }

    public string Id { get; set; }
}