$(document).ready(function (e) {

    var connection = new signalR.HubConnectionBuilder().withUrl(ApiBaseUrl + "message-hub").build();

    connection.start()
        .then(() => {
            console.log(connection.state)
        })
        .catch(err => {
            console.log(err)
        });

    $(".chat-user").click(function (e) {
        var element = $(this);
        var userId = element.attr("data-user-id");

        $.ajax({
            url: "/Admin/Chat/GetMessageListByUser?user=" + userId,
            type: 'GET',
            success: function (response) {
                $("#chat-history").html(response);
                console.log(response);

            }
        })
    });
    $("#message-input").keyup(function (e) {
        if (e.which == 13) {
            var val = $("#message-input").val();
            if (!val) return;

            var messageTemplate = $("#SenderMessage").html();
            var replacedMessage = messageTemplate.replace("{{Message}}", val);
            $("#message-list").append(replacedMessage);
            $("#message-input").val("");
            connection.invoke("SendMessageToCustomer", "user-connection:::1", val);
        }
    });
    connection.on("ReceiveMessageForCustomer", (message) => {

        var messageTemplate = $("#ReceiverMessage").html();
        var replacedMessage = messageTemplate.replace("{{Message}}", message);
        $("#message-list").append(replacedMessage);

    })
});