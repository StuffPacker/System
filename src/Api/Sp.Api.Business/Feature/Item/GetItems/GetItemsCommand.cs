using MediatR;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.GetItems;

public class GetItemsCommand : IRequest<List<ItemDto>>
{
    public GetItemsCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}