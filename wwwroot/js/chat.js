﻿/// SAMPLE CODE
/// SAMPLE CODE
/// SAMPLE CODE
"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
/// SAMPLE CODE
/// SAMPLE CODE
/// SAMPLE CODE


// Wait To Start
// Customize code to send to WaitToStart, pass in the variables
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    connection.invoke("WaitToStart", user).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// Send Initial Scores
connection.on("SendInitialScores", function (user, message) {
    // Abduls code
});

// Game Table
connection.on("GameBoard", function (user, message) {
    // Abduls code
});

// Waiting Message
connection.on("WaitingMessage", function (user, message) {
    // Abduls code
});

// Receive Word
// Customize code to send to WaitToStart, pass in the variables
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("ReceiveWord", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// Send Scores
connection.on("SendScores", function (user, message) {
    // Abduls code
});

// Results Screen
// Customize code to send to WaitToStart, pass in the variables
document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendResultScreen").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// Send Final Scores Lists
connection.on("SendFinalScores", function (user, message) {
    // Abduls code
});

// Send Words Lists
connection.on("SendWordLists", function (user, message) {
    // Abduls code
});

// Send Winner
connection.on("SendWinner", function (user, message) {
    // Abduls code
});