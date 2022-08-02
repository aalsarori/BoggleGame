using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
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
                string getplayers = "SELECT Player FROM game_session WHERE Game_Code = 1 ORDER BY Player ASC";

                // Run query

                // Make the loop variables
                string player_one = "";
                string player_two = "";

                // Make a for loop that sets the first to player_one and the second to player two

                // Insert player name into player table, set score = 0.
                string setplayers = string.Format("INSERT INTO games (usernameOne,usernameTwo,game_code) VALUES ('{0}','{1}', 1);", player_one, player_two);

                // Run the query

                // Get the game board and pass it in
                List<char> arr = new List<char>();

                for(int i = 0; i < 16; i++)
                {
                    arr.Add('b');
                }

                

                // Pass in initial names ands scores by drawing from database and passing them in
                string getinitialnamesandscores = "SELECT usernameOne, usernameTwo, userOneScore, userTwoScore FROM games WHERE game_code = 1";

                // Run the query

                // Set them equal to the variables
                string user1 = "usernameOne";
                string user2 = "usernameTwo";
                string score1 = "userOneScore";
                string score2 = "userTwoScore";

                // Pass it in
                await Clients.All.SendAsync("SendInitialScores", user1, score1, user2, score2);

                // Pass the array
                await Clients.All.SendAsync("GameBoard", arr);

            }


        }

        // Get the points to update
        public async Task ReceiveWord(string user, string word)
        {
            // Put the word into the database

            // Pull out each users name and score

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