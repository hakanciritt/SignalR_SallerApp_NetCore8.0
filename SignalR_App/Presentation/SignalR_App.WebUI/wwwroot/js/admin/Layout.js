$(document).ready(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl(ApiBaseUrl + "main-hub").build();

    connection.start()
        .then(() => {

            console.log(connection.state);
        })
        .catch(err => {
            console.log(err)
        });


});