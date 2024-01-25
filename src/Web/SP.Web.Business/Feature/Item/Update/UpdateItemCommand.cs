using MediatR;
using SP.Web.Business.Feature.Item.Update;

namespace SP.Web.Business.Feature.Item;

public class UpdateItemCommand : IRequest<ItemViewModel>
{
    public UpdateItemCommand(string id, Guid userId, ItemUpdateInputViewModel model)
    {
        Id = id;
        UserId = userId;
        Model = model;
    }

    public Guid UserId { get; set; }

    public string Id { get; set; }

    public ItemUpdateInputViewModel Model { get; set; }
}