using Sp.Api.Client.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.Mapper;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList;

public class PackingListService : IPackingListService
{
    private readonly IApiPackingListClient _apiPackingListClient;
    private readonly IPackingListMapper _packingListMapper;

    public PackingListService(IApiPackingListClient apiPackingListClient, IPackingListMapper packingListMapper)
    {
        _apiPackingListClient = apiPackingListClient;
        _packingListMapper = packingListMapper;
    }

    public async Task<PackingListViewModel> GetPackingListById(string id, Guid userId)
    {
       var model = await _apiPackingListClient.GetPackingList(id, userId);
       return PackingListViewModelMapper.Map(model);
    }

    public async Task<List<PackingListViewModel>> GetPackingLists(Guid userId)
    {
        var models = await _apiPackingListClient.GetPackingLists(userId);
        var list = new List<PackingListViewModel>();
        foreach (var item in models)
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
            Groups = new List<PackingListGroupModel>
            {
                new PackingListGroupModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "New Group",
                    Items = new List<PackingListGroupItemModel>()
                }
            }
        };
        var response = await _apiPackingListClient.Create(userId, model);
        return PackingListViewModelMapper.Map(response);
    }

    public async Task Delete(string id, Guid getUserId)
    {
        await _apiPackingListClient.Delete(id, getUserId);
    }

    public async Task Update(PackingListViewModel model, Guid userId)
    {
        await _apiPackingListClient.Update(PackingListViewModelMapper.Map(model), userId);
    }
}