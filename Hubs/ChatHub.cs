using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AbdulsGame.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Get the game to start
        public async Task WaitToStart(string user)
        {
            // Insert player name into the session table
            string insert = string.Format("INSERT INTO game_session (Game_Code,Player) VALUES (1,'{0}');", user);

            // Run the query





            // Check if count where gamecode = gamecode of game session >= 2.
            string check = string.Format("SELECT COUNT(*) FROM game_session WHERE Game_Code = 1;");
            int check_result = 0;

            // Get result of query





            if(check_result == 1 || check_result == 0)
            {
                // Send this to a function that will display a waiting screen
                await Clients.All.SendAsync("WaitingMessage");
            }
            else
            {
                // Build query
                string getplayers = "SELECT Player FROM game_session WHERE Game_Code = 1";

                // Run query

                // Make the loop variables
                string player_one = "";
                string player_two = "";

                // Make a for loop that sets the first to player_one and the second to player two

                // Insert player name into player table, set score = 0.
                string setplayers = string.Format("INSERT INTO games(usernameOne,usernameTwo) VALUES ('{0}','{1}');", player_one, player_two);

                // Run the query

                // Get the game board and pass it in

                // Pass in initial names ands scores by drawing from database and passing them in

            }


        }

        // Get the points to update
        public async Task ReceiveWord(string user, string word)
        {
            // Put the word into the database
            string executeWordGuessed = string.Format("SELECT dbo.WordGuessed(1, '{0}', '{1}');",user,word);
            //execute the executeWordGuessed string
            //if 0 is returned, word has already been guessed by user in current game
            //if -1 is returned, word is not a real word in dictionary table
            //if -2 is returned, username is invalid
            //if -3 is returned, gameNumber is invalid
            //if > 0 word is valid and not guessed yet. sp_UpdateUserScore then needs to be called to add word to guessed words list
            //      and add to user's score

            // Pull out each users name and score
            string getScore = string.Format("SELECT * FROM dbo.getUserScore(1,'{0}');",user);
            //getUserScore returns a table @Scores with columns: (UsernameOne,UsernameTwo,UserOneScore,UserTwoScore)
            // Assign them
            string user1 = "";
            string score1 = "";
            string user2 = "";
            string score2 = "";

            // Send them back
            await Clients.All.SendAsync("SendScoresUser1", user1, score1);
            await Clients.All.SendAsync("SendScoresUser2", user2, score2);
            // or
            await Clients.All.SendAsync("SendScores", user1, score1, user2, score2);

        }

        // Get the results passed in
        public async Task SendResultScreen(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Create the game board

    }
}