using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    // Receive a word
    public string ReceiveWord(string word)
    {
        return word;
    }

    // Send back the arrays
    public List<List<char>> SendArrays(List<List<char>> returnList)
    {
        return returnList;
    }

    // Send back the point totals
    public string SendPoints
    {
        get
        {
            return "";
        }
    }

    // Check to see if the word exists
    public string ValidateExistence(string word)
    {
        // Make query

        // Run query

        // See if it exists

        // Get points based on length

        // Add points

        return "";
    }

    // Add points equal to the length of the verified word
    public string AddPoints(int length)
    {
        // Query for the points

        // Add the points

        // Update the points

        // Or just update the points to be points + length if that is possible

        return "";
    }

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

    /// <summary>
    /// We also need to figure out how to do SignalR and time the match and end it at a certain time
    /// </summary>

    public void OnGet()
    {

    }
}