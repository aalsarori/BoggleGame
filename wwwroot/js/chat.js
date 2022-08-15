

/*const { start } = require("@popperjs/core");*/

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
    document.getElementById("endingthegame").addEventListener("click", function (event) {
        connection.invoke("SendResultScreen");
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


    connection.on("SendResultScreen", function () {

    });

    // Send Words Lists
    connection.on("SendWordLists", function (user1words, user2words) {

        // Abduls code
        //Display the words guessed by both users

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

        document.getElementById("winner").innerHTML = "The winner is " + winner
    });

    connection.on("GameBoard", function (arr) {

        for (i = 0; i < arr.length; i++) {

            var num = i + 1
            var c = document.getElementById(String(num));
            c.innerHTML = arr[i]

        }


        var grid1 = document.getElementById("1");
        if (grid1.innerHTML != 'a') {

            startTimer();
        }

        else {
            document.getElementById("winner").innerHTML = "Waiting for Player 2";
        }

    });

}



var container = document.getElementById("container");

var arr = ['a', 'k', 'h', 'b', 'd', 'q', 'p', 'c', 'd', 'a', 'l', 'g', 'h', 'v', 'm', 't'];
var cellsArr = [16]
var word = []
var clickedList = []
var secsLeft
var timer = null
var timeStarted = false
var letter = 0
var id = 1;
var clickedID = 0

// arrays to store the words guessed by each player

/*wordList1 = ['Hello', 'Abdul', 'Mike', 'Game']
wordList2 = ['Hello2', 'Abdul2', 'Mike2', 'Game2']*/

//arrays for each grid clickables
var one = [2, 5, 6]
var two = [1, 3, 5, 6, 7]
var three = [2, 6, 7, 8, 4]
var four = [3, 7, 8]
var five = [1, 2, 6, 9, 10]
var six = [1, 2, 3, 5, 7, 9, 10, 11]
var seven = [2, 3, 4, 6, 8, 10, 11, 12]
var eight = [3, 4, 7, 11, 12]
var nine = [5, 6, 10, 13, 14]
var ten = [5, 6, 7, 9, 11, 13, 14, 15]
var eleven = [6, 7, 8, 10, 12, 14, 15, 16]
var twelve = [7, 8, 11, 15, 16]
var thir = [9, 10, 14]
var forteen = [9, 10, 11, 13, 15]
var fif = [10, 11, 12, 14, 16]
var sixteen = [11, 12, 15]

makeRows(4, 4);

/*document.getElementById("startButton").addEventListener("click", checkPlayers);*/
document.getElementById("submit").addEventListener("click", refreshGrid);
document.getElementById("submitUser").addEventListener("click", showStart);

function showStart() {
    //document.getElementById("startButton").style.display = "block";
    document.getElementById("enterUsername").style.display = "none";
    document.getElementById("enterUsername1").style.display = "none";

}

function checkPlayers() {

    var grid1 = document.getElementById("1");
    if (grid1.innerHTML != 'a') {

        startTimer();
    }

    else {
        console.log("waiting for player 2");
    }

}


function makeRows(rows, cols) {
    container.style.setProperty('--grid-rows', rows);
    container.style.setProperty('--grid-cols', cols);
    for (var i = 0; i < (rows); i++) {
        for (var j = 0; j < cols; j++) {


            let cell = document.getElementById(String(id));
            cell.innerHTML = (arr[letter]);


            cell.addEventListener('mouseover', () => {

                cell.style.cursor = "pointer"
            });



            cell.style.width = "20%"

            cell.addEventListener('click', () => {

                if (clickedID > 0) {
                    makeUnclickable()
                }


                if (cell.style.backgroundColor == "red") {
                    cell.style.backgroundColor = "white";
                    cell.style.color = "black";

                }

                else {

                    cell.style.backgroundColor = "red";
                    cell.style.color = "white";
                    word.push(cell.innerHTML)

                }
            });

            letter++
            id++
            cellsArr[i * 4 + j] = cell

        }

    };

};


function tryit() {
    var p = document.getElementById("print")
    p.innerHTML = "";
    var i = 0
    while (i < word.length) {


        p = document.getElementById("print")
        p.innerHTML = p.innerHTML + word[i];
        i++;
    }

}




// timer to run the clock down
function startTimer() {

    document.getElementById("startButton").style.display = "none";
    show()

    if (timer) clearInterval(timer);
    // Timer that counts down 
    secsLeft = 60
    timer = setInterval(() => {

        secsLeft--;
        document.getElementById("secs").textContent = "00:" + Math.floor(secsLeft);
        if (secsLeft === 0) {
            hide();
            showScores()
            clearInterval(timer)
            document.getElementById("secs").textContent = "00:00"

        }
        else if (secsLeft <= 9) {
            document.getElementById("secs").textContent = "00:0" + Math.floor(secsLeft);
        }

    }, 1000);


}


function refreshGrid() {

    for (i = 1; i <= 16; i++) {
        cell = document.getElementById(String(i))

        cell.style.backgroundColor = "white";
        cell.style.color = "black";
        cell.style.pointerEvents = "auto";


        word = []
        clickedList = []
    }
}

function getClickID(clickID) {

    clickedID = clickID

    console.log(clickedID)

    clickedList.push(clickedID)

}



function makeUnclickable() {

    for (i = 0; i < 4; i++) {
        for (j = 0; j < 4; j++) {

            //makes everything unclickable
            cellsArr[i * 4 + j].style.pointerEvents = "none"

        }
    }

    makeClickable(clickedID)


};

function makeClickable(clickedID) {

    if (parseInt(clickedID) === 1) {

        for (k = 0; k < one.length; k++) {
            let cell = document.getElementById(String(one[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 2) {

        for (k = 0; k < two.length; k++) {
            let cell = document.getElementById(String(two[k]));
            cell.style.pointerEvents = "auto"
        }

    }

    else if (parseInt(clickedID) === 3) {

        for (k = 0; k < three.length; k++) {
            let cell = document.getElementById(String(three[k]));
            cell.style.pointerEvents = "auto"
        }

    }

    else if (parseInt(clickedID) === 4) {

        for (k = 0; k < four.length; k++) {
            let cell = document.getElementById(String(four[k]));
            cell.style.pointerEvents = "auto"
        }

    }

    else if (parseInt(clickedID) === 5) {

        for (k = 0; k < five.length; k++) {
            let cell = document.getElementById(String(five[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 6) {
        for (k = 0; k < six.length; k++) {
            let cell = document.getElementById(String(six[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 7) {
        for (k = 0; k < seven.length; k++) {
            let cell = document.getElementById(String(seven[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 8) {
        for (k = 0; k < eight.length; k++) {
            let cell = document.getElementById(String(eight[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 9) {
        for (k = 0; k < nine.length; k++) {
            let cell = document.getElementById(String(nine[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 10) {
        for (k = 0; k < ten.length; k++) {
            let cell = document.getElementById(String(ten[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 11) {
        for (k = 0; k < eleven.length; k++) {
            let cell = document.getElementById(String(eleven[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 12) {
        for (k = 0; k < twelve.length; k++) {
            let cell = document.getElementById(String(twelve[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 13) {
        for (k = 0; k < thir.length; k++) {
            let cell = document.getElementById(String(thir[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 14) {
        for (k = 0; k < forteen.length; k++) {
            let cell = document.getElementById(String(forteen[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 15) {
        for (k = 0; k < fif.length; k++) {
            let cell = document.getElementById(String(fif[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    else if (parseInt(clickedID) === 16) {
        for (k = 0; k < sixteen.length; k++) {
            let cell = document.getElementById(String(sixteen[k]));
            cell.style.pointerEvents = "auto"
        }
    }

    for (it = 0; it < clickedList.length; it++) {

        let cell = document.getElementById(String(clickedList[it]));
        cell.style.pointerEvents = "none"

    }

};


// show the grid
function show() {


    var style = document.createElement("style");
    document.head.appendChild(style);

    //Add rules to the style
    document.styleSheets[document.styleSheets.length - 1].insertRule(
        "div#container { \
          display: grid; \
       } ", 0);

}


// hide the grid
function hide() {

    document.getElementById("container").style.display = "none";

}

/*function showWordsGuessed() {

    var p = document.getElementById('finalScores1')
    var p2 = document.getElementById('finalScores2')
    for (i = 0; i < wordList1.length; i++) {

        if (i === 0) {
            p.innerHTML = wordList1[i] + '<br>'
            p2.innerHTML = wordList2[i] + '<br>'
        }
        else {
            p.innerHTML = p.innerHTML + wordList1[i] + '<br>'
            p2.innerHTML = p2.innerHTML + wordList2[i] + '<br>'
        }
    }
}*/

function showScores() {

    document.getElementById("endingthegame").style.display = "block";
}
