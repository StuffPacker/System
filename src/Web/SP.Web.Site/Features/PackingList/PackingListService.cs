using System.Security.Authentication;
using SP.Database.Mongo.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Site.Features.Item;
using SP.Web.Site.Features.Packinglist;

namespace SP.Web.Site.Features.PackingList;

public class PackingListService : IPackingListService
{
    private readonly IPackingListRepository _packingListRepository;

    public PackingListService(IPackingListRepository packingListRepository)
    {
        _packingListRepository = packingListRepository;
    }

    public async Task<PackingListViewModel> GetPackingListById(string id, Guid userId)
    {
        return await GetById(id, userId);
    }

    public async Task<List<PackingListViewModel>> GetPackingLists(Guid userId)
    {
        var result = await _packingListRepository.GetByUserId(userId);
        var list = new List<PackingListViewModel>();
        foreach (var item in result)
        {
            list.Add(new PackingListViewModel(item));
        }

        return list;
    }

    public async Task<PackingListViewModel> CreatePackingList(Guid userId)
    {
        var model = new PackingListModel
        {
            Name = "new packing list",
            UserId = userId,
            Groups = new List<PackListGroupModel>
            {
                new PackListGroupModel
                {
                    Name = "New Group",
                    Items = new List<PackingListGroupItemModel>()
                }
            }
        };
        var result = await _packingListRepository.Create(model);

        return new PackingListViewModel(result);
    }

    public async Task Delete(string id, Guid userId)
    {
        var model = await _packingListRepository.GetById(id);
        if (!Access(new PackingListViewModel(model), userId))
        {
            throw new AuthenticationException();
        }

        await _packingListRepository.Delete(id);
    }

    private async Task<PackingListViewModel> GetById(string id, Guid userId)
    {
        var result = await _packingListRepository.GetById(id);
        var vm = new PackingListViewModel(result);
        if (!Access(vm, userId))
        {
            return null!;
        }

        return vm;
    }

    private bool Access(PackingListViewModel model, Guid userId)
    {
        if (Guid.Parse(model.UserId) == userId)
        {
            return true;
        }

        return false;
    }
}