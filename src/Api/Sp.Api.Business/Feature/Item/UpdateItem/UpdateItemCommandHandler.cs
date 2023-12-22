using System.Security.Authentication;
using MediatR;
using SP.Shared.Common.Feature.Database.UserItem;

namespace Sp.Api.Business.Feature.Item.UpdateItem;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, string>
{
    private readonly IUserItemRepository _userItemRepository;

    public UpdateItemCommandHandler(IUserItemRepository userItemRepository)
    {
        _userItemRepository = userItemRepository;
    }

    public async Task<string> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        await UpdateItem(request.Id, request.UserId, request.InputModel);
        return string.Empty;
    }

    private async Task UpdateItem(string id, Guid userId, ItemEditInputDto inputModel)
    {
        var model = await _userItemRepository.GetById(id);
        if (model.UserId != userId)
        {
            throw new AuthenticationException();
        }

        model.Name = inputModel.Name;
        model.ChangeWeight(inputModel.Weight);
        await _userItemRepository.Update(model);
    }
}