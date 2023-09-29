function DashboardViewModel(id) {
    var self = this;
    var Items = [];
    self.Items = ko.observableArray(Items);
    DBVMGetAllItems(self);
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