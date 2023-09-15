using MediatR;

namespace SP.Web.Business.Feature.Item.UpdateItem;

public class UpdateItemCommand : IRequest<string>
{
    public UpdateItemCommand(string id, Guid userId, ItemEditInputViewModel inputModel)
    {
        Id = id;
        UserId = userId;
        InputModel = inputModel;
    }

    public ItemEditInputViewModel InputModel { get; set; }

    public Guid UserId { get; set; }

    public string Id { get; set; }
}