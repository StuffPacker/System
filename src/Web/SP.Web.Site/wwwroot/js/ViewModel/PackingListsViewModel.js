function PackingListsViewModel() {
    var self = this;
    var packingLists = [];
    self.PackingLists = ko.observableArray(packingLists);
    PLSVMGetAllPackingLists(self);
    self.Create = function ()
    {
        PLSVMCreatePackingList(self);
    }
}
function PLSVMCreatePackingList(self)
{
    SPApiPost('/api/v1/packinglist/',"", function (obj) {
        if (obj != null) {
            PLSVMAddPackingList(self,obj);
        }
    });
}
function PLSVMGetAllPackingLists(self)
{
    SPApiGet('/api/v1/packinglist/', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                PLSVMAddPackingList(self,dto);
            });
        }
    });
}
function PLSVMAddPackingList(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Link="/packinglist/" + dto.Id;
    item.Delete = function ()
    {
        SPApiDelete('/api/v1/packinglist/'+dto.Id,function (obj) {
            if (obj != null) {
                self.PackingLists.removeAll();
                PLSVMGetAllPackingLists(self);
            }
        });
    }   
    self.PackingLists.push(item);
}