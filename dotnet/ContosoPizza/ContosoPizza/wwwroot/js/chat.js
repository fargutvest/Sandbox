"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (cpu, ram) {
    var cpuEl = document.getElementById("cpu");
    var ramEl = document.getElementById("ram");
    cpuEl.textContent = `${cpu}`;
    ramEl.textContent = `${ram}`;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

var intervalId = window.setInterval(function () {
    connection.invoke("SendMessage").catch(function (err) {
        return console.error(err.toString());
    });
}, 1000);
