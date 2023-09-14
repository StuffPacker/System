function ItemViewModel(id) {
    var self = this;
    self.Name=ko.observable("")
    GetItem(self,id);

}
function GetItem(self,id)
{
  
    SPApiGet('/api/v1/items/'+id, function (obj) {
        if (obj != null) {           
               console.log(obj);
                self.Name(obj.Name);           
        }
    });
}