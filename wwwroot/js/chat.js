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
        var user = document.getElementById("username").value; // the user they enter will  go here
        var word = document.getElementById("print").innerHTML; // send each word they submit

        console.log(user)
        console.log(word)
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
    //document.getElementById("otherbuttons").addEventListener("click", function (event) {
    //    connection.invoke("SendResultScreen").catch(function (err) {
    //        return console.error(err.toString());
    //    });
    //    event.preventDefault();
    //});

    // Send Final Scores Lists
    connection.on("SendFinalScores", function (user1, score1, user2, score2) {
        // Abduls code

        document.getElementById("user1").innerHTML = "User: " + user1;
        document.getElementById("user2").innerHTML = "User: " + user2;
        document.getElementById("score1").innerHTML = "Final Score : " + score1;
        document.getElementById("score2").innerHTML = "Final Score : " + score2;
    });


    connection.on("SendResultScreen", function () {

    });

    // Send Words Lists
    connection.on("SendWordLists", function (user1words, user2words) {

        // Abduls code
        //YOOOOOOOOOOOOOOO
        console.log(user1words);
        console.log(user2words); // both lines are just for testing
        console.log("I exist!!!");
        //Dennissss

        //prototype for how to display the words guessed by both users

        var p = document.getElementById('finalScores1')
        var p2 = document.getElementById('finalScores2')

        for (i = 0; i < user1words.length; i++) {

            if (i === 0) {
                p.innerHTML = user1words[i] + '<br>'

            }
            else {
                p.innerHTML = p.innerHTML + user1words[i] + '<br>'

            }
        }


        for (i = 0; i < user2words.length; i++) {

            if (i === 0) {
                p2.innerHTML = user2words[i] + '<br>'
            }
            else {
                p2.innerHTML = p2.innerHTML + user2words[i] + '<br>'
            }
        }
    });

    // Send Winner
    connection.on("SendWinner", function (winner) {
        // Abduls code

        document.getElementById("winner").innerHTML = winner
    });

    connection.on("GameBoard", function (arr) {

        for (i = 0; i < arr.length; i++) {

            var num = i + 1
            var c = document.getElementById(String(num));
            c.innerHTML = arr[i]

        }
    });

}