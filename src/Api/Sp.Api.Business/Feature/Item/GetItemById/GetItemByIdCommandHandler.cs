using System.Security.Authentication;
using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.Item.Mapper;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.GetItemById;

public class GetItemByIdCommandHandler : IRequestHandler<GetItemByIdCommand, ItemDto>
{
    private readonly IUserItemRepository _userItemRepository;
    private readonly IItemModelMapper _itemModelMapper;

    public GetItemByIdCommandHandler(IUserItemRepository userItemRepository, IItemModelMapper itemModelMapper)
    {
        _userItemRepository = userItemRepository;
        _itemModelMapper = itemModelMapper;
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

        return _itemModelMapper.Map(model);
    }
}