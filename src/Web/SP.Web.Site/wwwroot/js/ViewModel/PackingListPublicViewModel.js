function PackingListPublicViewModel(id) {
    var self = this;
    var Groups = [];
    self.Name = ko.observable("");
    self.Description=ko.observable("");
    self.Groups = ko.observableArray(Groups);
    self.PrintLink = ko.observable("/packinglist/" + id + "/print");
    PLPVMGetPackingList(self, id);
}
function PLPVMGetPackingList(self,id)
{
    SPApiGet('/api/v1/packinglist/'+id+'/public', function (obj) {
        if (obj != null) {
            self.Name(obj.Name);  
            self.Description(obj.Description);
            ko.utils.arrayForEach(obj.Groups, function (dto) {
                PLPVMAddGroup(self,id,dto);
            });
        }
    });
}
function PLPVMAddGroup(self,id,dto)
{
    var items = [];
    var item = new Object();
    item.Name=dto.Name;
    item.Id=dto.Id;
    item.EditGroupName = ko.observable(false);    
    item.Items=ko.observableArray(items);
    ko.utils.arrayForEach(dto.Items, function (dto2) {
        PLPVMAddItem(self,item,id,item.Id,dto2);
    }); 
    self.Groups.push(item);
}
function PLPVMAddItem(self,group,id,groupid,dto)
{
    var item = new Object();
    item.Id=dto.Id;
    item.Name=dto.Name;
    item.Weight=dto.Weight;
    item.WeightSufix=dto.WeightSufix;    
    item.Quantity=dto.Quantity;   
    group.Items.push(item);
}

