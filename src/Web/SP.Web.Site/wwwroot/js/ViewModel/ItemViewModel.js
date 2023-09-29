function ItemViewModel(id) {
    var self = this;
    self.Name=ko.observable("")
    IVMGetItem(self,id);

}
function IVMGetItem(self,id)
{
  
    SPApiGet('/api/v1/items/'+id, function (obj) {
        if (obj != null) {           
               console.log(obj);
                self.Name(obj.Name);           
        }
    });
}