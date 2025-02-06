$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

    connection.on("ReloadData", function () {
        location.reload();
    });

    connection.start().then().catch(function (err) {
        return console.error(err.toString());
    });


})
