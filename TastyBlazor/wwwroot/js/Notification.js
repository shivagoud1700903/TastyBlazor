window.ShowToastr = function (type, Message) {
    if (type === "success")
        toastr.success(Message);
    if (type === "error")
        toastr.error(Message);
}