$(document).ready(function () {
    $(".btn-outline-danger").click(function (e) {

        var id = $(this).attr("data-id");

        Swal.fire({
            title: "Emin misin?",
            text: "Silmek istediğnize emin misiniz?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır"
        }).then((result) => {

            if (result.isConfirmed) {
                $.ajax({
                    url: "/Admin/TextContent/Delete?textContentId=" + Number(id),
                    method: "GET",
                    success: function (response) {

                        if (response.success) {

                            Swal.fire({
                                title: "Deleted!",
                                text: "Başarıyla silindi",
                                icon: "success"
                            }).then(result => {
                                if (result.isConfirmed) document.location = "/Admin/TextContent";
                            });

                            document.location = "/Admin/TextContent";
                        } else {
                            Swal.fire({
                                title: "Error",
                                text: response.errorMessage,
                                icon: "error"
                            });
                        }
                    },
                    error: function (request, status, error) {

                        Swal.fire({
                            title: "Error",
                            text: request.responseText,
                            icon: "error"
                        });
                    }
                });


            }
        });

    });
});