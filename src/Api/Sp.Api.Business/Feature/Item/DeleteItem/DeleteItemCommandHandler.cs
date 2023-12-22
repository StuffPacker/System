using System.Security.Authentication;
using MediatR;
using Sp.Api.Business.Feature.Item.DeleteItem;
using SP.Shared.Common.Feature.Database.UserItem;

namespace SP.Web.Business.Feature.Item.DeleteItem;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, string>
{
    private readonly IUserItemRepository _userItemRepository;

    public DeleteItemCommandHandler(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<string> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        await Delete(request.Id, request.UserId);
        return string.Empty;
    }

    private async Task Delete(string id, Guid userId)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        await _userItemRepository.Delete(id);
    }
}