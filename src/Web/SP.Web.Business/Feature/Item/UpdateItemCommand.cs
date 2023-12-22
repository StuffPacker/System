using MediatR;

namespace SP.Web.Business.Feature.Item;

public class UpdateItemCommand : IRequest<List<ItemViewModel>>
{
    public UpdateItemCommand(string id, Guid userId, ItemEditInputViewModel model)
    {
        Id = id;
        UserId = userId;
        Model = model;
    }

    public Guid UserId { get; set; }

    public string Id { get; set; }

    public ItemEditInputViewModel Model { get; set; }
}