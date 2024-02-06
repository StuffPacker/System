using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Model;

namespace Sp.Api.Business.Feature.PackingList;

public interface IPackingListService
{
    Task<PackingListDto> GetPackingListById(string id);

    Task<List<PackingListDto>> GetPackingLists(Guid userId);

    Task<PackingListDto> CreatePackingList(Guid userId, PackingListModel model);

    Task Delete(string id, Guid getUserId);

    Task Update(PackingListDto model);

    Task<List<PackingListDto>> GetPackingListsPublic();
}