function HomeViewModel(id) {
    var self = this;   
    var packingListsPublic = [];
    self.PackingListsPublic = ko.observableArray(packingListsPublic);   
    HVMGetAllPackingListsPublic(self);
}
function HVMGetAllPackingListsPublic(self)
{
    SPApiGet('/api/v1/packinglist/public/packinglist', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                HVMAddPackingListPublic(self,dto);
            });
        }
    });
}
function HVMAddPackingListPublic(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Link="/packinglist/" + dto.Id+"/public/";
    item.Lang=dto.Language;
    item.FlagUrl="/img/flag/"+item.Lang+".png";
    self.PackingListsPublic.push(item);
}