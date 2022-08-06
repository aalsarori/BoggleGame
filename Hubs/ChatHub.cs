using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                db = new SqlCommand(setplayers, connection);
                db.ExecuteNonQuery();

                // Get the game board and pass it in
                List<char> arr = new List<char>();
                arr = wordsRemix;

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

            // Open the connection
            string connectionString = "Server=titan.cs.weber.edu, 10433;Database=AmandaShow;User ID=AmandaShow;Password=+h1sIsthenewP@ssword!";
            connection = new SqlConnection(connectionString);
            connection.Open();

            // See if the user already guessed the word
            string executeWordGuessed = string.Format("SELECT dbo.WordGuessed(1, '{0}', '{1}');",user,word);
            //execute the executeWordGuessed string
            SqlCommand db = new SqlCommand(executeWordGuessed, connection);

            int check_result = 0;
            check_result = (int)db.ExecuteScalar();

            //if 0 is returned, word has already been guessed by user in current game
            //if -1 is returned, word is not a real word in dictionary table
            //if -2 is returned, username is invalid
            //if -3 is returned, gameNumber is invalid
            //if > 0 word is valid and not guessed yet. sp_UpdateUserScore then needs to be called to add word to guessed words list
            //      and add to user's score
            string executeUpdateUserScore = string.Format("EXEC sp_UpdateUserScore @gameNumber = 1, @username = '{0}', @score = '{1}', @word = '{2}' GO",user,check_result,word);
            if (check_result > 0)
            {
                db = new SqlCommand(executeUpdateUserScore, connection);
                db.ExecuteNonQuery();
            }
            // Pull out each users name and score
            string getScore = "SELECT usernameOne, usernameTwo, userOneScore, userTwoScore FROM games WHERE game_code = 1";
            db = new SqlCommand(getScore, connection);

            // Assign them
            List<string> newScores = new List<string>();
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

            // set them to variables
            string user1 = newScores[0];
            string user2 = newScores[1];
            string score1 = newScores[2];
            string score2 = newScores[3];

            // Send them back
            await Clients.All.SendAsync("SendScores", user1, score1, user2, score2);

        }

        // Get the results passed in
        public async Task SendResultScreen(string user, string message)
        {
            // Open the connection
            string connectionString = "Server=titan.cs.weber.edu, 10433;Database=AmandaShow;User ID=AmandaShow;Password=+h1sIsthenewP@ssword!";
            connection = new SqlConnection(connectionString);
            connection.Open();

            // Get the scores from the users
            // Pass in initial names ands scores by drawing from database and passing them in
            string getinitialnamesandscores = "SELECT usernameOne, usernameTwo, userOneScore, userTwoScore FROM games WHERE game_code = 1";

            // Create a list to store values to
            List<string> newScores = new List<string>();

            // Run the query
            SqlCommand db = new SqlCommand(getinitialnamesandscores, connection);
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

            // Set them equal to the variables
            string user1 = newScores[0];
            string user2 = newScores[1];
            string score1 = newScores[2];
            string score2 = newScores[3];

            // Pass it in
            await Clients.All.SendAsync("SendFinalScores", user1, score1, user2, score2);

            // Get the words from the users
            List<string> user1words = new List<string>();
            List<string> user2words = new List<string>();
            string getuser1words = string.Format("SELECT word FROM guesses WHERE username = '{0}' ", user1);
            string getuser2words = string.Format("SELECT word FROM guesses WHERE username = '{0}' ", user2);

            // Run the query
            db = new SqlCommand(getuser1words, connection);
            db.CommandType = CommandType.Text;

            using (SqlDataReader objReader = db.ExecuteReader())
            {
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        string item = objReader.GetString(objReader.GetOrdinal("word"));
                        user1words.Add(item);
                    }
                }
            }

            // Run the query
            db = new SqlCommand(getuser2words, connection);
            db.CommandType = CommandType.Text;

            using (SqlDataReader objReader = db.ExecuteReader())
            {
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        //I would also check for DB.Null here before reading the value.
                        string item = objReader.GetString(objReader.GetOrdinal("word"));
                        user2words.Add(item);
                    }
                }
            }

            await Clients.All.SendAsync("SendWordLists", user1words, user2words);

            // Decide the winner
            string winner = "";

            if(int.Parse(score1) > int.Parse(score2))
            {
                winner = user1;
            }
            else if (int.Parse(score1) < int.Parse(score2))
            {
                winner = user2;
            }
            else
            {
                winner = "TIE";
            }

            await Clients.All.SendAsync("SendWinner", winner);

            connection.Close();
        }

        // Create the game board

        // Return a random consonant
        public char RandomConsonant(string word)
        {
            // Make the list of consonants
            char[] consonants = "BCDFGHJKLMNPQRSTVWXYZ".ToCharArray();

            // Make the random number
            Random rnd = new Random();
            int randomnumber = rnd.Next(0, (consonants.Length)); // creates a number between 1 and 12

            // Choose the random consonant
            char randomchar = consonants[randomnumber];

            // Use recursion until the word doesn't already contain that character
            if (word.Contains(randomchar))
            {
                randomchar = RandomConsonant(word);
            }

            // Return the value
            return randomchar;
        }

        // Return a random vowel
        public char RandomVowel
        {
            get
            {
                // Make the list of vowels
                char[] vowels = "AEIOU".ToCharArray();

                // Make the random number
                Random rnd = new Random();
                int randomnumber = rnd.Next(0, (vowels.Length)); // creates a number between 1 and 12

                // Choose the random consonant
                char randomchar = vowels[randomnumber];

                // Return the value
                return randomchar;
            }
        }

        // Make the words to send to Tans function for him to scramble around
        public List<string> CreateWords
        {
            get
            {
                // Create the variables
                string first = "";
                string second = "";
                string third = "";
                string fourth = "";

                List<string> words = new List<string>();

                // Add the vowels
                first += RandomVowel;
                first += RandomVowel;
                first += RandomConsonant(first);
                first += RandomConsonant(first);

                second += RandomVowel;
                second += RandomVowel;
                second += RandomConsonant(second);
                second += RandomConsonant(second);

                third += RandomVowel;
                third += RandomVowel;
                third += RandomConsonant(third);
                third += RandomConsonant(third);

                fourth += RandomVowel;
                fourth += RandomConsonant(fourth);
                fourth += RandomConsonant(fourth);
                fourth += RandomConsonant(fourth);

                // Add the four words to an array
                words.Add(first);
                words.Add(second);
                words.Add(third);
                words.Add(fourth);

                // Return the array
                return words;
            }
        }

        public char[] wordRemix(string word)
        {
            char[] arrayRemix = word.ToCharArray();
            Random ran = new Random();
            int a = arrayRemix.Length;
            while (a > 1)
            {
                a--;
                int b = ran.Next(a + 1);
                var temp = arrayRemix[b];
                arrayRemix[b] = arrayRemix[a];
                arrayRemix[a] = temp;
            }
            return arrayRemix;
        }

        // Create function (call function)
        public List<char> wordsRemix
        {
            get
            {
                List<char> tempList = new List<char>();

                List<string> word = CreateWords;

                // Loop through the four words (for each)
                foreach (string a in word)
                {
                    // Pass each one into wordRemix
                    char[] charlist = wordRemix(a);

                    foreach (char letter in charlist)
                    {
                        // Add the arrayRemix it returns to a bigger array
                        tempList.Add(letter);
                    }
                }

                //check for duplicates
                List<char> returnList = tempList.Distinct().ToList();

                // check for 16 chars
                int wordcount = 0;
                foreach (char letter in returnList)
                {
                    wordcount++;
                }

                while (wordcount < 16)
                {
                    returnList.Add(RandomVowel);
                    wordcount++;
                }

                // return array
                return returnList;
            }
        }

    }
}