using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Database.Mongo.Feature.PackingList;

public interface IPackingListRepository
{
    Task<PackingListModel> Create(PackingListModel model);

    Task<PackingListModel> GetById(string id);

    Task Update(PackingListModel model);

    Task Delete(string id);

    Task<IEnumerable<PackingListModel>> GetByUserId(Guid userId);

    Task<IEnumerable<PackingListModel>> GetPublic();
}