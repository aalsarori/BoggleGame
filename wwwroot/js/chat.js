"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Wait To Start
// Customize code to send to WaitToStart, pass in the variables
window.onload = function () {
    document.getElementById("startButton").addEventListener("click", function (event) {
        document.getElementById("user1").innerHTML = "Usersss: ";
        var user = "asdf";
        connection.invoke("WaitToStart", user).catch(function (err) {

        });
        event.preventDefault();
    });

    // Send Initial Scores
    connection.on("SendInitialScores", function (user1, score1, user2, score2) {
        // Abduls code

        //grab users and their scores and insert them into the html display <p> designated for them
        document.getElementById("user1").innerHTML = "User: " + user1;
        document.getElementById("user2").innerHTML = "User: " + user2;
        document.getElementById("score1").innerHTML = "Score : " + score1;
        document.getElementById("score2").innerHTML = "Score : " + score2;
    });

    // Waiting Message
    connection.on("WaitingMessage", function () {
        // Abduls code
    });

    // Receive Word
    // Customize code to send to WaitToStart, pass in the variables
    document.getElementById("otherbuttonss").addEventListener("click", function (event) {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        connection.invoke("ReceiveWord", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

    // Send Scores
    connection.on("SendScores", function (user1, score1, user2, score2) {
        // Abduls code
        document.getElementById("user1").innerHTML = "User: " + user1;
        document.getElementById("user2").innerHTML = "User: " + user2;
        document.getElementById("score1").innerHTML = "Score : " + score1;
        document.getElementById("score2").innerHTML = "Score : " + score2;
    });

    // Results Screen
    // Customize code to send to WaitToStart, pass in the variables
    document.getElementById("otherbutton").addEventListener("click", function (event) {
        connection.invoke("SendResultScreen").catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

    // Send Final Scores Lists
    connection.on("SendFinalScores", function (user1, score1, user2, score2) {
        // Abduls code

        document.getElementById("user1").innerHTML = "User: " + user1;
        document.getElementById("user2").innerHTML = "User: " + user2;
        document.getElementById("score1").innerHTML = "Final Score : " + score1;
        document.getElementById("score2").innerHTML = "Final Score : " + score2;
    });

    // Send Words Lists
    connection.on("SendWordLists", function (user1words, user2words) {
        // Abduls code
    });

    // Send Winner
    connection.on("SendWinner", function (winner) {
        // Abduls code

        document.getElementById("winner").innerHTML = winner
    });
}