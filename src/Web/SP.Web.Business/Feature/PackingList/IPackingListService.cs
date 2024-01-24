using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList;

public interface IPackingListService
{
    Task<PackingListViewModel> GetPackingListById(string id, Guid userId);

    Task<List<PackingListViewModel>> GetPackingLists(Guid userId);

    Task<PackingListViewModel> CreatePackingList(Guid userId);

    Task Delete(string id, Guid getUserId);

    Task Update(PackingListViewModel model, Guid userId);
}