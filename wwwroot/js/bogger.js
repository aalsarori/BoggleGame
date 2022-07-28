const container = document.getElementById("container");

var arr = ['a', 'k', 'h', 'b', 'd', 'q', 'p', 'c', 'd', 'a', 'l', 'g', 'h', 'v', 'm', 't'];
var word = []
var secsLeft
var timer = null

function makeRows(rows, cols) {
  container.style.setProperty('--grid-rows', rows);
  container.style.setProperty('--grid-cols', cols);
  for (c = 0; c < (rows * cols); c++) {
    let cell = document.createElement("div");
    cell.innerText = (arr[c]);

    container.appendChild(cell).id = "grid-item" + c;

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

      }
    });

  };
};

makeRows(4, 4);


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



  if (timer) clearInterval(timer);
  // Timer that counts down 
  secsLeft = 50;
  timer = setInterval(() => {

    secsLeft--;
    document.getElementById("secs").textContent = "00:" + Math.floor(secsLeft);
    if (secsLeft === 0) {
      clearInterval(timer);
      document.getElementById("secs").textContent = "00:00"
    }
    else if (secsLeft <= 9) {
      document.getElementById("secs").textContent = "00:0" + Math.floor(secsLeft);
    }

  }, 1000);

}
