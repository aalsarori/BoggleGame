const container = document.getElementById("container");

var arrLetters = ['a', 'k', 'h', 'b', 'd', 'q', 'p', 'c', 'd', 'a', 'l', 'g', 'h', 'v', 'm', 't'];
var cellsArr = [16]
var word = []
var secsLeft
var timer = null
var timeStarted = false
var letter = 0
var id


// Game Table
/*connection.on("GameBoard", function (arr) {
    // Abduls code

    for (i = 0; i < 16; i++) {
        arrLetters[i] = arr[i]
    }
});
*/


function makeRows(rows, cols) {
    container.style.setProperty('--grid-rows', rows);
    container.style.setProperty('--grid-cols', cols);
    for (i = 0; i < (rows); i++) {
        for (j = 0; j < cols; j++) {


            let cell = document.createElement("div");
            cell.innerText = (arrLetters[letter]);



            cell.tagName = i + " " + j;

            container.appendChild(cell).id = "i " + i + " " + j;

            cell.addEventListener('mouseover', () => {

                cell.style.cursor = "pointer"
            });



            cell.style.width = "20%"



            cell.addEventListener('click', () => {


                if (cell.style.backgroundColor == "red") {
                    cell.style.backgroundColor = "white";
                    cell.style.color = "black";

                }

                else {

                    cell.style.backgroundColor = "red";
                    cell.style.color = "white";
                    word.push(cell.innerText)
                    id = cell.id

                }
            });

            letter++
            cellsArr[i * 4 + j] = cell

        }

    };





};
makeRows(4, 4);
makeUnclickable(1, 0)



function tryit() {
  var i = 0
  while (i < word.length) {

    var p = document.getElementById("print")
    p.innerHTML = p.innerHTML + word[i];
    i++;
  }

}


document.getElementById("startButton").addEventListener("click", startTimer);

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
      clearInterval(timer)
      document.getElementById("secs").textContent = "00:00"
      
    }
    else if (secsLeft <= 9) {
      document.getElementById("secs").textContent = "00:0" + Math.floor(secsLeft);
    }

  }, 1000);


}

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


function makeUnclickable(i, j) {

    cellsArr[i * 4 + j].replaceWith(cellsArr[i * 4 + j].cloneNode(true));

};

function makeClickable(i, j) {

    var id = "i " + i + " " + j;
    cell = document.getElementById(id)

    cell.addEventListener('mouseover', () => {

        cell.style.cursor = "pointer"
    });

    cell.addEventListener('click', () => {


        if (cell.style.backgroundColor == "red") {
            cell.style.backgroundColor = "white";
            cell.style.color = "black";

        }

        else {

            cell.style.backgroundColor = "red";
            cell.style.color = "white";
            word.push(cell.innerText)

        }
    });

}



// hide the grid
function hide() {

    document.getElementById("container").style.display = "none";

}
