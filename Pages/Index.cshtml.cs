using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    // Receive a word

    // Send back the arrays

    // Send back the point totals

    // Check to see if the word exists

    // Add points equal to the length of the verified word

    // Get the random words

    /// <summary>
    /// We also need to figure out how to do SignalR and time the match and end it at a certain time
    /// </summary>

    public void OnGet()
    {

    }
}