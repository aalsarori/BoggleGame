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
            // Start the connection
            string connectionString = "Server=titan.cs.weber.edu, 10433;Database=AmandaShow;User ID=AmandaShow;Password=+h1sIsthenewP@ssword!";
            connection = new SqlConnection(connectionString);
            connection.Open();

            // Add the user to the game session

            // Get a count of players in the game session

            // If the count is less than 2, send Waiting Message

            // If the count is greater or equal to 2, get the scores

            // Then get the gameboard

            // Then pass them both in

            //await Clients.All.SendAsync("WaitingMessage");

            // Close the connection
            connection.Close();

            // Pretend it already works
            await Clients.All.SendAsync("SendInitialScores", "Fake1", "0", "Fake2", "0");

            List<char> arr = new List<char>();
            arr = wordsRemix;
            await Clients.All.SendAsync("GameBoard", arr);
        }

        // Get the points to update
        public async Task ReceiveWord(string user, string word)
        {
            // Open the connection
            string connectionString = "Server=titan.cs.weber.edu, 10433;Database=AmandaShow;User ID=AmandaShow;Password=+h1sIsthenewP@ssword!";
            connection = new SqlConnection(connectionString);
            connection.Open();

            // Set the query up for ReceiveWord

            // Run the query

            // Select the scores and names for each user

            // Set them equal to variables

            // Close the connection
            connection.Close();

            // Pretend it works
            // Send the data from the set variables
            await Clients.All.SendAsync("SendScores", "Fake1", "99", "Fake2", "99");
        }

        // Get the results passed in
        public async Task SendResultScreen()
        {
            // Open the connection
            string connectionString = "Server=titan.cs.weber.edu, 10433;Database=AmandaShow;User ID=AmandaShow;Password=+h1sIsthenewP@ssword!";
            connection = new SqlConnection(connectionString);
            connection.Open();

            // Create the query for getting final scores

            // Create the query for getting the word lists

            // Run the query for getting final scores

            // Set them equal to variables

            // Run the query for getting word lists

            // Set them equal to variables

            // Find the winner based on the score, and set it equal to a variable

            // Consider using timers between queries

            // Clear the game sessions list and the word list and the score list

            // Pass it in
            await Clients.All.SendAsync("SendFinalScores", "Fake1", "100", "Fake2", "101");
            List<string> test1 = new List<string>();
            List<string> test2 = new List<string>();

            test1.Add("Guess1");
            test2.Add("Guess2");
            test1.Add("Guess3");
            test2.Add("Guess4");

            // Close the connection
            connection.Close();

            // Send the relevant data
            await Clients.All.SendAsync("SendWordLists", test1, test2);
            await Clients.All.SendAsync("SendWinner", "Fake2");
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