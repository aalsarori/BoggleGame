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
        public async Task WaitToStart(string user, string gamecode)
        {
            // Add the username to the player / score list, with a score of 0.

            // Add a count to the game session.

            // Check the count of the game session.

            // If it is 1, return await Clients.All.SendAsync("Wait", user, message);
                // Make it so that it just says Waiting for another player... 1/2

            // If it is 2, return await Clients.All.SendAsync("Start", user, message);  
                // Get the game board and pass it in

                // Pass in the timer some how

                // Pass in initial names ands scores by drawing from database and passing them in
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
        }

        // Get the results passed in
        public async Task SendResultScreen(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Create the game board

    }
}