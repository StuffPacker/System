function ManageResultData(obj) {
    const code = obj.Meta.Code;
    if (code == "200") {
        return obj.ResultData;
    }
    if (code == "400") {
        alert("Error valedating data", obj.ResultData);
        return null;
    }
    if (code == "404") {
        window.location.replace("/404");
    }
    else {
        alert("A error has accured, please try later");
    }
}
function SPApiGet(url, callback) {

    fetch(url)
        .then(response => response.json())
        .then(response => {

            var obj = ManageResultData(response);
            callback(obj);
        });
}
