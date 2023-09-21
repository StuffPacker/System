function PackingListPublicViewModel(id) {
    var self = this;
    var Groups = [];
    self.Name = ko.observable("");
    self.Groups = ko.observableArray(Groups);

    GetPackingList(self, id);
}
function GetPackingList(self,id)
{
    SPApiGet('/api/v1/packinglist/'+id+'/public', function (obj) {
        if (obj != null) {
            self.Name(obj.Name);  
            ko.utils.arrayForEach(obj.Groups, function (dto) {
                AddGroup(self,id,dto);
            });
        }
    });
}
function AddGroup(self,id,dto)
{
    var items = [];
    var item = new Object();
    item.Name=dto.Name;
    item.Id=dto.Id;
    item.EditGroupName = ko.observable(false);    
    item.Items=ko.observableArray(items);
    ko.utils.arrayForEach(dto.Items, function (dto2) {
        AddItem(self,item,id,item.Id,dto2);
    }); 
    self.Groups.push(item);
}
function AddItem(self,group,id,groupid,dto)
{
    var item = new Object();
    item.Id=dto.Id;
    item.Name=dto.Name;
    item.Weight=dto.Weight;
    item.WeightSufix=dto.WeightSufix;    
    item.Quantity=dto.Quantity;   
    group.Items.push(item);
}

