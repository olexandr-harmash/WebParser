namespace WebParsing;

public class Utils
{
    /// <summary>
    /// Calculates the Term Frequency (TF) of a word in a document.
    /// </summary>
    /// <param name="wordCount">The number of times the word appears in the document.</param>
    /// <param name="totalWords">The total number of words in the document.</param>
    /// <returns>The TF value of the word.</returns>
    public static double CalculateTF(int wordCount, int totalWords)
    {
        if (totalWords == 0)
        {
            throw new ArgumentException("Total word count cannot be zero.");
        }

        return (double)wordCount / totalWords;
    }

    /// <summary>
    /// Calculates the Inverse Document Frequency (IDF) for a word.
    /// </summary>
    /// <param name="documentCount">The total number of documents.</param>
    /// <param name="documentsWithWord">The number of documents containing the word.</param>
    /// <returns>The IDF value of the word.</returns>
    public static double CalculateIDF(int documentCount, int documentsWithWord)
    {
        if (documentCount == 0)
        {
            throw new ArgumentException("Document count cannot be zero.");
        }

        return Math.Log((double)documentCount / documentsWithWord); // Add 1 to avoid division by zero
    }

    /// <summary>
    /// Calculates the cosine similarity between two vectors.
    /// </summary>
    /// <param name="vectorA">The first vector.</param>
    /// <param name="vectorB">The second vector.</param>
    /// <returns>The cosine similarity value between the two vectors.</returns>
    public static double CalculateCosineSimilarity(double[] vectorA, double[] vectorB)
    {
        if (vectorA.Length != vectorB.Length)
        {
            throw new ArgumentException("Vectors must be of the same length.");
        }

        double dotProduct = 0;
        double magnitudeA = 0;
        double magnitudeB = 0;

        for (int i = 0; i < vectorA.Length; i++)
        {
            dotProduct += vectorA[i] * vectorB[i];
            magnitudeA += Math.Pow(vectorA[i], 2);
            magnitudeB += Math.Pow(vectorB[i], 2);
        }

        magnitudeA = Math.Sqrt(magnitudeA);
        magnitudeB = Math.Sqrt(magnitudeB);

        if (magnitudeA == 0 || magnitudeB == 0)
        {
            throw new InvalidOperationException("One or both vectors have zero magnitude, cannot calculate cosine similarity.");
        }

        return dotProduct / (magnitudeA * magnitudeB);
    }

    /// <summary>
    /// Converts two dictionaries into two vectors, where each key corresponds to an index in the vector,
    /// and the values are filled according to the key's values from the dictionaries. 
    /// If a key is missing in one of the dictionaries, the corresponding vector position is filled with zero.
    /// </summary>
    /// <param name="dict1">The first dictionary containing keys and their associated integer values.</param>
    /// <param name="dict2">The second dictionary containing keys and their associated integer values.</param>
    /// <returns>A tuple containing two double arrays (vectors), where the first array corresponds to the first dictionary,
    /// and the second array corresponds to the second dictionary.</returns>
    public static (double[] Vector1, double[] Vector2) ConvertDictionariesToVectors(Dictionary<string, int> dict1, Dictionary<string, int> dict2)
    {
        // Get the union of keys from both dictionaries
        var allKeys = dict1.Keys.Union(dict2.Keys).ToList();

        // Create vectors with a size equal to the number of unique keys
        var vector1 = new double[allKeys.Count];
        var vector2 = new double[allKeys.Count];

        // Fill the vectors with values from the dictionaries
        for (int i = 0; i < allKeys.Count; i++)
        {
            var key = allKeys[i];
            // Use TryGetValue to retrieve values, default to 0 if the key is not found
            vector1[i] = dict1.TryGetValue(key, out var value1) ? value1 : 0;
            vector2[i] = dict2.TryGetValue(key, out var value2) ? value2 : 0;
        }

        // Return the filled vectors
        return (vector1, vector2);
    }
}
