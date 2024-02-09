$(document).ready(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl(ApiBaseUrl + "message-hub").build();

    connection.start()
        .then(() => {

            $("#connstatus").text(connection.state);
        })
        .catch(err => {
            console.log(err)
        });
    $("#send-message").click(function (e) {

        var message = $("#message").val();
        connection.invoke("SendMessageToAdmin", message);

        var messageTemplate = $("#SenderMessage").html();
        var replacedMessage = messageTemplate.replace("{{Message}}", message);
        $("#chat-body").append(replacedMessage);
        $("#message").val("");
    });
    $("#message").keyup(function (e) {
        if (e.which == 13) {
            $("#send-message").click();
        }
    });
    connection.on("ReceiveMessageForAdmin", (message) => {

        var messageTemplate = $("#ReceiverMessage").html();
        console.log(messageTemplate);
        console.log(message);


        var replacedMessage = messageTemplate.replace("{{Message}}", message);
        $("#chat-body").append(replacedMessage);
    })
});

