$(document).ready(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl(ApiBaseUrl + "message-hub").build();

    connection.start()
        .then(() => {

            $("#connstatus").text(connection.state);

        })
        .catch(err => {
            console.log(err)
        });

    $("#send-button").click(function (e) {
        var user = document.getElementById("user-input").value;
        var message = document.getElementById("message").value;
        connection.invoke("SendMessage", user, message);

        e.preventDefault();
    });

    connection.on("ReceiveMessage", (user, message) => {
        var li = document.createElement("li");
        var span = document.createElement("span");
        span.style.fontWeight = "bold";
        span.textContent = user;
        li.appendChild(span);
        li.innerHTML += `: ${message}`;

        var messageList = document.getElementById("message-list");

        messageList.appendChild(li);
    });


});

