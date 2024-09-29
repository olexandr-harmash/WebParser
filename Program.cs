// ============================================================================
// Project Name: Document Analysis
// Author: [Ваше имя]
// Date: [Дата создания]
// Description:
// 
// This project is designed to analyze text documents by calculating
// Term Frequency (TF), Inverse Document Frequency (IDF), and cosine similarity
// between the word frequency distributions of two documents. The implementation
// currently retrieves text from web pages using HtmlAgilityPack and processes
// it using basic regular expressions.
// 
// Key Features:
// - Calculates TF and IDF for specified words in the documents.
// - Computes cosine similarity between the frequency vectors of two documents.
// - Handles basic text parsing and tokenization to extract words from the documents.
// 
// Potential Improvements:
// 1. **Parser Service**: Implement a dedicated service for parsing HTML documents
//    instead of directly using the HtmlAgilityPack parser. This would allow for
//    more flexible and configurable parsing strategies tailored to specific use cases.
// 
// 2. **Advanced Tokenization Algorithms**: Incorporate more sophisticated tokenization
//    algorithms to improve the accuracy of word detection. This may include handling
//    different languages, punctuations, and special characters more effectively.
// 
// 3. **Optimized Vector Calculations**: Instead of continuously calculating vectors 
//    based on two dictionaries, consider consolidating all document dictionaries 
//    into a single master dictionary. This would allow for easier retrieval of word 
//    frequencies and ensure that vector calculations are performed on a consistent 
//    basis, reducing redundancy and improving efficiency.
// 
// 4. **Thorough Testing**: While the current implementation works, detailed testing
//    has not been performed. There may be discrepancies and inaccuracies due to
//    the simplistic use of regular expressions for data extraction, especially in
//    the presence of complex HTML structures or unexpected characters.
//
// 5. **Enhance**: To enhance the efficiency and accuracy of calculating TF, IDF, and cosine similarity,
//    consider leveraging the Microsoft.ML library. This library offers powerful tools for text processing and analysis,
//    including tokenization and vectorization methods, which can significantly simplify the implementation of these algorithms.
// 
// This project is a starting point for further development and refinement of
// text analysis algorithms and methodologies.
// ============================================================================

namespace WebParsing;

public class Program
{
    public static void Main()
    {
        // Создаем список документов для анализа
        List<Document> documents = new List<Document>
        {
            new Document("https://en.wikipedia.org/wiki/Russo-Ukrainian_War"),
            new Document("https://en.wikipedia.org/wiki/Russo-Japanese_War")
        };

        // Получаем количество документов
        var documentCount = documents.Count();

        // Анализируем первый документ
        var firstDocument = documents[0];
        var targetWord = "ukrainian";
        var targetWordCount = firstDocument.WordOccurrences.ContainsKey(targetWord)
            ? firstDocument.WordOccurrences[targetWord]
            : 0;

        // Вычисляем Term Frequency (TF) для целевого слова
        var termFrequency = Utils.CalculateTF(targetWordCount, firstDocument.TotalWords);
        Console.WriteLine($"Term Frequency (TF) of '{targetWord}': {termFrequency}");

        // Подсчитываем количество документов, содержащих целевое слово
        var documentsWithTargetWord = documents.Select(doc => doc.WordOccurrences.ContainsKey(targetWord) ? doc.WordOccurrences[targetWord] : 0);
        var documentsWithTargetWordCount = documentsWithTargetWord.Count(count => count > 0);

        // Вычисляем Inverse Document Frequency (IDF) для целевого слова
        var inverseDocumentFrequency = Utils.CalculateIDF(documentCount, documentsWithTargetWordCount);
        Console.WriteLine($"Inverse Document Frequency (IDF) of '{targetWord}': {inverseDocumentFrequency}");

        // Анализируем второй документ
        var secondDocument = documents[1];

        // Преобразуем словари частот слов в векторы
        var (vector1, vector2) = Utils.ConvertDictionariesToVectors(firstDocument.WordOccurrences, secondDocument.WordOccurrences);

        // Вычисляем косинусное сходство между векторами
        var cosineSimilarity = Utils.CalculateCosineSimilarity(vector1, vector2);
        Console.WriteLine($"Cosine Similarity between documents: {cosineSimilarity}");
    }
}
