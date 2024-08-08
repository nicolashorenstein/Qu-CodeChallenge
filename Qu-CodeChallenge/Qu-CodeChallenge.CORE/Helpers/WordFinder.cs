using Qu_CodeChallenge.CORE.Interfaces.Helper;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.CORE.Helpers;

public class WordFinder : IWordFinder
{
    private List<string> _matrix;
    private static int[,] directions = { { 0, 1 }, { 1, 0 } }; // Right, Down
    private int rows, cols;
    
    public WordFinder(List<string> matrix)
    {
        _matrix = matrix;
    }
    public async Task<WordFinderResult> Find(List<string> wordstream)
    {
        var result = new WordFinderResult();
        var matrix = InitializeMatrix(_matrix);
        var wordCount = new Dictionary<string, int>();
        if (matrix == null || wordstream == null || wordstream.Count == 0)
            return result;

        rows = matrix.GetLength(0);
        cols = matrix.GetLength(1);

        var trie = new Trie();
        foreach (var word in wordstream)
        {
            trie.Insert(word);
            wordCount[word] = 0;  // Initialize counts to 0
        }

        var root = trie.GetRoot();
        var visited = new bool[rows, cols]; // Create a visited matrix

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                // Separate search for each direction
                Search(matrix, r, c, root, "", wordCount, visited, 0); // Right direction
                Search(matrix, r, c, root, "", wordCount, visited, 1); // Down direction
            }
        }

        result.Words = wordCount;
        return result;
    }
    
    private void Search(string[,] matrix, int row, int col, TrieNode node, string word, Dictionary<string, int> wordCount, bool[,] visited, int direction)
    {
        if (row < 0 || row >= rows || col < 0 || col >= cols || visited[row, col] || !node.Children.ContainsKey(matrix[row, col][0]))
            return;

        visited[row, col] = true; // Mark the cell as visited
        char ch = matrix[row, col][0];
        node = node.Children[ch];
        word += ch;

        if (node.IsEndOfWord)
        {
            wordCount[word]++;  // Increment the count for the found word
            // If word found, reset visited to allow overlapping only in new words
            visited[row, col] = false;
            return;
        }

        if (direction == 0) // Right
        {
            Search(matrix, row, col + 1, node, word, wordCount, visited, direction);
        }
        else if (direction == 1) // Down
        {
            Search(matrix, row + 1, col, node, word, wordCount, visited, direction);
        }

        visited[row, col] = false; // Unmark the cell after backtracking

    }
    
        
    private string[,] InitializeMatrix(List<string> matrixList)
    {
        var rows = matrixList.Count;
        var cols = matrixList[0].Length;
        var stringMatrix = new string[rows,cols];
         for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                stringMatrix[i, j] = matrixList[i][j].ToString().ToLower();
                Console.Write(stringMatrix[i, j] + " ");
            }
            Console.WriteLine(); 
        }
    
        return stringMatrix;
    }
    
   
}