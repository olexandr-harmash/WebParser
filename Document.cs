using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace WebParsing;

public class Document 
{
    /// <summary>
    /// Dictionary containing the frequency of word occurrences in the document.
    /// Keys are the words, and values represent the number of occurrences.
    /// </summary>
    public Dictionary<string, int> WordOccurrences { get; private set; }

    /// <summary>
    /// URL of the document to be parsed.
    /// </summary>
    public string Url { get; private set; }

    /// <summary>
    /// Total words in the document.
    /// </summary>
    public int TotalWords { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class.
    /// Downloads the page content, tokenizes the inner text, and calculates word frequencies.
    /// </summary>
    /// <param name="url">The URL of the document to parse.</param>
    public Document(string url) 
    {
        Url = url ?? throw new ArgumentNullException(nameof(url), "URL cannot be null.");

        // Retrieve the inner text from the page.
        var innerText = GetPageText();

        // Tokenize the inner text into individual words.
        var tokens = TokenizeInnerText(innerText);

        TotalWords = tokens.Count();

        // Convert the tokens into a dictionary of word frequencies.
        WordOccurrences = CalculateWordFrequencies(tokens);
    }

    /// <summary>
    /// Prints the word occurrences dictionary to the console.
    /// Each word and its count will be displayed on a new line.
    /// </summary>
    public void PrintWordOccurrences() 
    {
        foreach (var entry in WordOccurrences) 
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    /// <summary>
    /// Asynchronously retrieves the inner text from the body of a web page specified by the URL.
    /// 
    /// For complex parsing scenarios, an additional service layer can be implemented with a
    /// specialized HTML parsing engine and customizable parsing configurations based on specific tags.
    /// </summary>
    /// <returns>
    /// The inner text of the body element from the HTML document.
    /// Throws an exception if the body cannot be loaded or is empty.
    /// </returns>
    private string GetPageText() 
    {
        var web = new HtmlWeb();

        try 
        {
            // Load the HTML document from the given URL.
            var htmlDocument = web.Load(Url);
            var htmlBody = htmlDocument.DocumentNode.SelectSingleNode("//body");

            if (htmlBody == null) 
            {
                throw new Exception("Can't load the body of the document.");
            }

            var innerText = htmlBody.InnerText;

            if (string.IsNullOrWhiteSpace(innerText)) 
            {
                throw new Exception("Body is empty.");
            }

            return innerText;
        } 
        catch (Exception ex) 
        {
            Console.WriteLine($"Error loading page: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Tokenizes the inner text of a document by extracting words while handling escape sequences
    /// and ensuring the input is valid.
    /// </summary>
    /// <param name="innerText">The text to be tokenized. It should not be null or empty.</param>
    /// <returns>A list of tokens (words) extracted from the inner text.</returns>
    private List<string> TokenizeInnerText(string innerText) 
    {
        try 
        {
            if (string.IsNullOrWhiteSpace(innerText)) 
            {
                throw new ArgumentException("Input text cannot be null or empty.", nameof(innerText));
            }

            var preparedText = innerText.ToLower();

            // Match words containing alphabetic characters (both Latin and Cyrillic).
            var matches = Regex.Matches(preparedText, @"[^\W\d_]{3,}");

            if (matches.Count == 0) 
            {
                throw new InvalidOperationException("No tokens found in the input text.");
            }

            // Extract words from the match results and return as a list.
            var tokens = matches.Select(w => w.Value).ToList();

            return tokens;
        } 
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Converts a list of words into a dictionary where the keys are words and the values are their frequencies.
    /// </summary>
    /// <param name="words">A list of words to be converted into a dictionary with word counts.</param>
    /// <returns>A dictionary where each word is a key, and its value is the number of occurrences of that word in the list.</returns>
    private Dictionary<string, int> CalculateWordFrequencies(List<string> words) 
    {
        var wordCountDictionary = new Dictionary<string, int>();

        foreach (var word in words) 
        {
            // Increment the word count if the word already exists in the dictionary, otherwise add it.
            if (wordCountDictionary.ContainsKey(word)) 
            {
                wordCountDictionary[word]++;
            } 
            else 
            {
                wordCountDictionary[word] = 1;
            }
        }

        return wordCountDictionary;
    }
}
