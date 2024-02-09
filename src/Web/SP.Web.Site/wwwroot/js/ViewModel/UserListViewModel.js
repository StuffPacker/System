function UserListViewModel(id) {
    var self = this;
    var users = [];
    self.Users=ko.observableArray(users);
    UsrLVMLoadUsers(self);
}
function UsrLVMLoadUsers(self)
{
    SPApiGet('/api/v1/user/', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                UsrLVMAddUsers(self,dto);
            });
        }
    });   
}
function UsrLVMAddUsers(self,dto)
{
    var obj=new Object();
    obj.Name=dto.Name;
    obj.Id=dto.Id;
    obj.ProfileUrl="/user/"+dto.Id;
    self.Users.push(obj);
}