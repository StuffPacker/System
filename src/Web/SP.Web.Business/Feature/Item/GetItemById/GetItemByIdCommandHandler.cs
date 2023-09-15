using System.Security.Authentication;
using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;

namespace SP.Web.Business.Feature.Item.GetItemById;

public class GetItemByIdCommandHandler : IRequestHandler<GetItemByIdCommand, ItemViewModel>
{
    private readonly IUserItemRepository _userItemRepository;

    public GetItemByIdCommandHandler(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<ItemViewModel> Handle(GetItemByIdCommand request, CancellationToken cancellationToken)
    {
        return await GetItemById(request.Id, request.UserId);
    }

    private async Task<ItemViewModel> GetItemById(string id, Guid userId)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        return new ItemViewModel(model);
    }
}