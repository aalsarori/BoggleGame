"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start();
//Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true;
//connection.on("ReceiveMessage", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);
//    // We can assign user-supplied strings to an element's textContent because it
//    // is not interpreted as markup. If you're assigning in any other way, you 
//    // should be aware of possible script injection concerns.
//    li.textContent = `${user} says ${message}`;
//});
//connection.start().then(function () {
//    document.getElementById("sendButton").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});
//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});
/// SAMPLE CODE
/// SAMPLE CODE
/// SAMPLE CODE


// Wait To Start
// Customize code to send to WaitToStart, pass in the variables
window.onload = function () {
    document.getElementById("submitUser").addEventListener("click", function (event) {
        var user = document.getElementById("username").value; // username they enter goes here as well
        console.log(user); // prints the user to the console for testing purposes
        connection.invoke("WaitToStart", user);
        event.preventDefault();
        
    });


    // Send Initial Scores
    connection.on("SendInitialScores", function (user1, score1, user2, score2) {
        // Abduls code

        //grab users and their scores and insert them into the html display <p> designated for them
        document.getElementById("user1").innerHTML = "User: " + user1;
        document.getElementById("user2").innerHTML = "User: " + user2;
        document.getElementById("score1").innerHTML = "Score: " + score1;
        document.getElementById("score2").innerHTML = "Score : " + score2;
    });

    // Waiting Message
    connection.on("WaitingMessage", function () {
        // Abduls code
    });

    // Receive Word
    // Customize code to send to WaitToStart, pass in the variables
    
    document.getElementById("submit").addEventListener("click", function (event) {
        var user = "FakeUser"; // the user they enter will  go here
        var word = "Word"; // send each word they submit
        //check the word the user is sending
        connection.invoke("ReceiveWord", user, word);
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
    document.getElementById("otherbuttons").addEventListener("click", function (event) {
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


    //recive the letters for grid
    connection.on("GameBoard", function (arr) {

    });
}