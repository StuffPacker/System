using SP.Shared.Common.Feature.PackingList.Model;

namespace Sp.Api.Client.Feature.PackingList;

public interface IApiPackingListClient
{
    Task<PackingListModel> GetPackingList(string id, Guid currentUser);

    Task<PackingListModel> GetPackingListPublic(string requestId);

    Task<PackingListModel> GetPackingListPrint(string requestId);

    Task<IEnumerable<PackingListModel>> GetPackingLists(Guid userId);

    Task<PackingListModel> Create(Guid userId, PackingListModel model);

    Task Delete(string id, Guid getUserId);

    Task Update(PackingListModel model, Guid userId);
}