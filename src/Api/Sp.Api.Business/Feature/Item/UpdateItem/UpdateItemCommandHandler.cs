using System.Security.Authentication;
using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;
using SP.Shared.Common.Feature.Item.Mapper;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.Item.UpdateItem;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemDto>
{
    private readonly IUserItemRepository _userItemRepository;
    private readonly IItemModelMapper _itemModelMapper;

    public UpdateItemCommandHandler(IUserItemRepository userItemRepository, IItemModelMapper itemModelMapper)
    {
        _userItemRepository = userItemRepository;
        _itemModelMapper = itemModelMapper;
    }

    public async Task<ItemDto> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        return await UpdateItem(request.Id, request.UserId, request.InputModel);
    }

    private async Task<ItemDto> UpdateItem(string id, Guid userId, ItemEditInputDto inputModel)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        model.Name = inputModel.Name;
        model.ChangeWeight(inputModel.Weight);
        await _userItemRepository.Update(model);
        return _itemModelMapper.Map(model);
    }
}