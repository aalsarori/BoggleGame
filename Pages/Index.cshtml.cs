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
            second += RandomVowel;
            third += RandomVowel;
            fourth += RandomVowel;

            // Add the consonants
            for(int i = 0; i < 3; i++)
            {
                // Add a random consonant
                first += RandomConsonant(first);
                second += RandomConsonant(second);
                third += RandomConsonant(third);
                fourth += RandomConsonant(fourth);
            }

            // Add the four words to an array
            words.Add(first);
            words.Add(second);
            words.Add(third);
            words.Add(fourth);

            // Return the array
            return words;
        }
    }
    
    //public char[] wordRemix(string word)
    //{
    //    char[] arrayRemix = word.ToCharArray();
    //    Random ran = new Random();
    //    int a = array.Length;
    //    while(a > 1)
    //    {
    //        a--;
    //        int b = ran.Next(a+1);
    //        var temp = arrayRemix[b];
    //        arrayRemix[b] = arrayRemix[a];
    //        arrayRemix[a] = temp;
    //    }
    //    return arrayRemix;
    //}

    // Create function (call function)
    public List<char> wordsRemix
    {
        get
        {
            List<char> returnList = new List<char>();

            List<string> word = CreateWords;

            // Loop through the four words (for each)
            foreach(string a in word)
            { 
                // Pass each one into wordRemix
                //char[] charlist = wordRemix(word);

                /*foreach(char letter in charlist)
                {                   
                    // Add the arrayRemix it returns to a bigger array
                   // returnList.Add(arrayRemix);
                }*/
            }

            // Return the array
            return returnList;
        }   
    }



    /// <summary>
    /// We also need to figure out how to do SignalR and time the match and end it at a certain time
    /// </summary>

    public void OnGet()
    {

    }
}