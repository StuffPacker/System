using MediatR;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.CreateItem;

public class CreateItemCommand : IRequest<ItemDto>
{
    public CreateItemCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}