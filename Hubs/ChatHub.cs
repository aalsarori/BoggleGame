using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AbdulsGame.Hubs
{
    public class ChatHub : Hub
    {
        public SqlConnection connection;

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Get the game to start
        public async Task WaitToStart(string user)
        {
            // Open the connection
            string connectionString = "Server=titan.cs.weber.edu, 10433;Database=AmandaShow;User ID=AmandaShow;Password=+h1sIsthenewP@ssword!";
            connection = new SqlConnection(connectionString);
            connection.Open();

            // Insert player name into the session table
            string insert = string.Format("INSERT INTO game_session (Game_Code,Player) VALUES (1,'{0}');", user);

            // Run the query
            SqlCommand db = new SqlCommand(insert, connection);
            db.ExecuteNonQuery();


            // Check if count where gamecode = gamecode of game session >= 2.
            string check = string.Format("SELECT COUNT(*) FROM game_session WHERE Game_Code = 1;");
            int check_result = 0;

            // Get result of query
            db = new SqlCommand(check, connection);
            check_result = (int)db.ExecuteScalar();

            // Check results, act accordingly
            if(check_result == 1 || check_result == 0)
            {
                // Send this to a function that will display a waiting screen
                await Clients.All.SendAsync("WaitingMessage");
            }
            else
            {
                // Build query
                string getplayers = "SELECT Player FROM game_session WHERE Game_Code = 1 ORDER BY Player ASC";

                // Create a list to store values to
                List<string> newPlayers = new List<string>();

                // Run the query
                db = new SqlCommand(getplayers, connection);
                db.CommandType = CommandType.Text;
                using (SqlDataReader objReader = db.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            //I would also check for DB.Null here before reading the value.
                            string item = objReader.GetString(objReader.GetOrdinal("Player"));
                            newPlayers.Add(item);
                        }
                    }
                }

                // Make the loop variables
                string player_one = newPlayers[0];
                string player_two = newPlayers[1];

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
                string getinitialnamesandscores = "SELECT usernameOne, usernameTwo, userOneScore, userTwoScore FROM games WHERE game_code = 1"; // Build query

                // Create a list to store values to
                List<string> newScores = new List<string>();

                // Run the query
                db = new SqlCommand(getinitialnamesandscores, connection);
                db.CommandType = CommandType.Text;
                using (SqlDataReader objReader = db.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            //I would also check for DB.Null here before reading the value.
                            string item = objReader.GetString(objReader.GetOrdinal("usernameOne"));
                            newScores.Add(item);

                            item = objReader.GetString(objReader.GetOrdinal("usernameTwo"));
                            newScores.Add(item);

                            item = objReader.GetString(objReader.GetOrdinal("userOneScore"));
                            newScores.Add(item);

                            item = objReader.GetString(objReader.GetOrdinal("userTwoScore"));
                            newScores.Add(item);
                        }
                    }
                }

                // If that doesn't work, query each individually
                // Set them equal to the variables
                string user1 = newScores[0];
                string user2 = newScores[1];
                string score1 = newScores[2];
                string score2 = newScores[3];

                // Pass it in
                await Clients.All.SendAsync("SendInitialScores", user1, score1, user2, score2);

                // Pass the array
                await Clients.All.SendAsync("GameBoard", arr);
            }

            // Close connection.
            connection.Close();
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
            // Get the scores from the users

            // Get the words from the users

            // Decide the winner

            // Send them all out

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Create the game board

    }
}