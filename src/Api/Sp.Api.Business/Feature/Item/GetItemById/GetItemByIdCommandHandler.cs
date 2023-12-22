using System.Security.Authentication;
using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.GetItemById;

public class GetItemByIdCommandHandler : IRequestHandler<GetItemByIdCommand, ItemDto>
{
    private readonly IUserItemRepository _userItemRepository;

    public GetItemByIdCommandHandler(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<ItemDto> Handle(GetItemByIdCommand request, CancellationToken cancellationToken)
    {
        return await GetItemById(request.Id, request.UserId);
    }

    private async Task<ItemDto> GetItemById(string id, Guid userId)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        return null!; // _itemModelMapper.Map(model);
    }
}