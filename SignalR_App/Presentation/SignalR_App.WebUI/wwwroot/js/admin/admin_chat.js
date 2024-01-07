$(document).ready(function (e) {

    var connection = new signalR.HubConnectionBuilder().withUrl(ApiBaseUrl + "message-hub").build();

    connection.start()
        .then(() => {

            $("#connstatus").text(connection.state);
        })
        .catch(err => {
            console.log(err)
        });
    //$("#send-message").click(function (e) {

    //var message = $("#message").val();
    connection.invoke("SendMessageToCustomer", "user-connection:::1", "merhaba size nasıl yardımcı olabilirim acaba");

    //var messageTemplate = $("#SenderMessage").html();
    //var replacedMessage = messageTemplate.replace("{{Message}}", message);
    /*    $("#chat-body").append(replacedMessage);*/
    //$("#message").val("");d
    //});
    //$("#message").keyup(function (e) {
    //    if (e.which == 13) {
    //        $("#send-message").click();
    //    }
    //});
    //connection.on("ReceiveMessageForCustomer", (message) => {

    //    var messageTemplate = $("#ReceiverMessage").html();
    //    var replacedMessage = messageTemplate.replace("{{Message}}", message);
    //    $("#chat-body").append(replacedMessage);
    //})

});