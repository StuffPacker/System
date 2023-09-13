using SP.Web.Site.Features.Packinglist;

namespace SP.Web.Site.Features.PackingList;

public interface IPackingListService
{
    Task<PackingListViewModel> GetPackingListById(string id, Guid userId);

    Task<List<PackingListViewModel>> GetPackingLists(Guid userId);

    Task<PackingListViewModel> CreatePackingList(Guid userId);

    Task Delete(string id, Guid getUserId);

    Task Update(PackingListViewModel model);
}