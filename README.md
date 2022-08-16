# BoggleGame

![boggle game](https://user-images.githubusercontent.com/55166509/184927648-f494a160-848b-44bc-a220-a13116f63c0b.JPG)



# Description 
An asynchronous boggle multiplayer game. Game requires at least 2 players for the game to start. Both players have the same grid and a timer of 60 seconds.
Player with more guessed words wins the game.

# Tools Used

.NET: Server side functionality used .NET <br/>
SignalR: We ran our asynchronous tasks through SignalR, which mostly uses websockets <br/>
Database: Data was stored in a MYSQL database. <br/>
Front End: Front end used HTML, CSS, and JavaScript (JS). <br/>

# My contributions

I worked on the front end of the project. I created the letters board as grids using HTML and then added event listeners using JS.
Further, made the game timer wait for the second player to join the game, before the game would start. Moreover, I created functions to hide the grid once the
timer reached zero. 

# Challenges: 

The letters board should only allow for adjacent and diagnoal letter to be clicked. I solved the problem by giving each grid an HTML ID and after the user clicks
a letter, it only open diagnoal/adjacent grids.

# Game functionality

Both players see the same grid.  When the game starts, both players can find words within the grid.  A word can be formed by using a sequence of adjacent/diagonal letters, no one square being used more than once in a word.  For example, "THEM" is a valid word in this grid, but "HAH" is not. 

The grid should be selected so that vowels have a 40% chance of generating.  If you want to tweak your letters so that more common letters show up more than others (such as T showing up more than Q), feel free to do so, but it is not required. 

Words should be compared server side against an already existing list of valid dictionary words before accepted.

When a user enters a correct word, a corresponding indication to the other player that the opponent found a word. 

No word can be used twice (even if it shows up twice in different areas on the grid) 

Words must be at least 3 letters long

How words are scored :
3 letters - 1 point
4 letters - 2 points
5 letters - 4 points
6 letters - 6 points
7 letters - 8 points
8 letters - 10 points
9+ letters - 15 points

At the end of the game, each player should see a results screen.  The screen lists for each player all words correctly found, as well as that players total score.  The winner is displayed. 
