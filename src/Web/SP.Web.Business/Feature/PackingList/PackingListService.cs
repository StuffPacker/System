using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList;

public class PackingListService : IPackingListService
{
    public Task<PackingListViewModel> GetPackingListById(string id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PackingListViewModel>> GetPackingLists(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<PackingListViewModel> CreatePackingList(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task Delete(string id, Guid getUserId)
    {
        throw new NotImplementedException();
    }

    public Task Update(PackingListViewModel model)
    {
        throw new NotImplementedException();
    }
}