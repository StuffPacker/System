function DashboardViewModel(id) {
    var self = this;
    var Items = [];
    self.Items = ko.observableArray(Items);
    var packingListsPublic = [];
    self.PackingListsPublic = ko.observableArray(packingListsPublic);
    DBVMGetAllItems(self);
    var packingLists = [];
    self.PackingLists = ko.observableArray(packingLists);
    DBVMGetAllPackingLists(self);
    DBVMGetAllPackingListsPublic(self);
    
}
function DBVMGetAllPackingLists(self)
{
    SPApiGet('/api/v1/packinglist/', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                DBVMAddPackingList(self,dto);
            });
        }
    });
}
function DBVMAddPackingListPublic(self,dto)
{  
    var item = new Object();
    item.Name=dto.Name;
    item.Link="/packinglist/" + dto.Id+"/public/";
    item.Lang=dto.Language;     
        item.FlagUrl="/img/flag/"+item.Lang+".png";   
    self.PackingListsPublic.push(item);
}
function DBVMGetAllPackingListsPublic(self)
{
    SPApiGet('/api/v1/packinglist/public/packinglist', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                DBVMAddPackingListPublic(self,dto);
            });
        }
    });
}
function DBVMGetAllItems(self)
{
    SPApiGet('/api/v1/items/', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                DBVMAddItem(self,dto);
            });
        }
    });
}
function DBVMAddItem(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Weight=dto.Weight;
    item.WeightSufix=dto.WeightSufix;
    item.Link="/item/"+dto.Id;
    self.Items.push(item);
}
function DBVMAddPackingList(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Link="/packinglist/" + dto.Id;    
    self.PackingLists.push(item);
}