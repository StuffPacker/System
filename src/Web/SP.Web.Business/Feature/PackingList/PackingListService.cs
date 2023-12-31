using System.Security.Authentication;
using SP.Database.Mongo.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.Mapper;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList;

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
            list.Add(PackingListViewModelMapper.Map(item));
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
                    Id = Guid.NewGuid().ToString(),
                    Name = "New Group",
                    Items = new List<PackingListGroupItemModel>()
                }
            }
        };
        var result = await _packingListRepository.Create(model);

        return PackingListViewModelMapper.Map(result);
    }

    public async Task Delete(string id, Guid userId)
    {
        var model = await _packingListRepository.GetById(id);
        if (!Access(PackingListViewModelMapper.Map(model), userId))
        {
            throw new AuthenticationException();
        }

        await _packingListRepository.Delete(id);
    }

    public async Task Update(PackingListViewModel viewModel)
    {
        var model = GetModel(viewModel);
        await _packingListRepository.Update(model);
    }

    private PackingListModel GetModel(PackingListViewModel viewModel)
    {
        return new PackingListModel
        {
            UserId = Guid.Parse(viewModel.UserId),
            Name = viewModel.Name,
            Groups = GetGroupModel(viewModel.Groups),
            Id = viewModel.Id,
            IsPublic = viewModel.IsPublic
        };
    }

    private List<PackListGroupModel> GetGroupModel(List<PackingListGroupViewModel> viewModelGroups)
    {
        var list = new List<PackListGroupModel>();
        foreach (var item in viewModelGroups)
        {
            list.Add(new PackListGroupModel
            {
                Name = item.Name,
                Items = GetItems(item.Items),
                Id = item.Id
            });
        }

        return list;
    }

    private List<PackingListGroupItemModel> GetItems(List<PackingListGroupItemViewModel> itemItems)
    {
        var list = new List<PackingListGroupItemModel>();
        foreach (var item in itemItems)
        {
            list.Add(new PackingListGroupItemModel
            {
                Name = item.Name,
                WeightSufix = item.WeightSufix,
                RefId = item.Id,
                Weight = Convert.ToDecimal(item.Weight),
                Quantity = item.Quantity
            });
        }

        return list;
    }

    private async Task<PackingListViewModel> GetById(string id, Guid userId)
    {
        var result = await _packingListRepository.GetById(id);
        var vm = PackingListViewModelMapper.Map(result);
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