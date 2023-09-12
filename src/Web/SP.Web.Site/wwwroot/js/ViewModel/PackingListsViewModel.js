function PackingListsViewModel() {
    var self = this;
    var packingLists = [];
    self.PackingLists = ko.observableArray(packingLists);
    GetAllPackingLists(self);
    self.Create = function ()
    {
        CreatePackingList(self);
    }
}
function CreatePackingList(self)
{
    SPApiPost('/api/v1/packinglist/',"", function (obj) {
        if (obj != null) {
            AddPackingList(self,obj);
        }
    });
}
function GetAllPackingLists(self)
{
    SPApiGet('/api/v1/packinglist/', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                AddPackingList(self,dto);
            });
        }
    });
}
function AddPackingList(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Link="/packinglist/" + dto.Id;
    item.Delete = function ()
    {
        SPApiDelete('/api/v1/packinglist/'+dto.Id,function (obj) {
            if (obj != null) {
                self.PackingLists.removeAll();
                GetAllPackingLists(self);
            }
        });
    }   
    self.PackingLists.push(item);
}