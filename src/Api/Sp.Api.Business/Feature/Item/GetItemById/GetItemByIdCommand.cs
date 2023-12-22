using MediatR;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.GetItemById;

public class GetItemByIdCommand : IRequest<ItemDto>
{
    public GetItemByIdCommand(string id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public Guid UserId { get; set; }

    public string Id { get; set; }
}