$(document).ready(function () {

    function FailAlert(errorMessage) {
        Swal.fire({
            title: "Hata",
            text: errorMessage ? errorMessage : "Bir hata meydana geldi",
            icon: "error"
        });
    }

    function SuccessAlert(title) {
        Swal.fire({
            title: "Başarılı",
            text: title ? title : "",
            icon: "success"
        });
    }

    function WarningAlert(infoMessage) {
        Swal.fire({
            title: "Emin misin?",
            text: infoMessage ? infoMessage : "Silmek istediğnize emin misiniz?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır"
        }).then((result) => {

            if (result.isConfirmed) {
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success"
                });
            }
        });
    }

});