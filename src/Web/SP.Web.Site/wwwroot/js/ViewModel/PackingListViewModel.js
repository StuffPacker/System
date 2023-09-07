function PackingListViewModel(id) {
    var self = this;
    var Groups = [];
    self.Name = ko.observable("");
    self.Groups = ko.observableArray(Groups);
    GetPackingList(self,id);
}
function GetPackingList(self,id)
{    
    SPApiGet('/api/v1/packinglist/'+id, function (obj) {
        if (obj != null) {
            self.Name(obj.Name);            
            ko.utils.arrayForEach(obj.Groups, function (dto) {
                AddGroup(self,dto);
            });
        }
    });
}
function AddGroup(self,dto)
{
    var items = [];
    var item = new Object();
    item.Name=dto.Name;
   item.Items=ko.observableArray(items);
   console.log(dto);
     ko.utils.arrayForEach(dto.Items, function (dto2) {
         AddItem(item,dto2);
     });
    self.Groups.push(item);
}
function AddItem(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Weight=dto.Weight;
    item.WeightSufix=dto.WeightSufix;
    self.Items.push(item);
}
