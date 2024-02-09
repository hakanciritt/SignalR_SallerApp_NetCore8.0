function addOrUpdateFailure(err) {
    console.error(err);
}
function addOrUpdateSuccess(response) {
    if (response.success) {
        Swal.fire({
            title: "Successed",
            text: "",
            icon: "success"
        });
        document.location = "/Admin/Product";
    } else {
        Swal.fire({
            title: "Error!",
            text: response.message ? response.message : "Bir hata meydana geldi",
            icon: "error"
        });
    }
}

function beginRequest() {

}
function completeRequest() {
}