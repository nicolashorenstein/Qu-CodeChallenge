using System.Text;
using Qu_CodeChallenge.CORE.Interfaces.Matrix;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.CORE.Services.Matrix;

public class MatrixService : IMatrixService
{
    public async Task<MatrixResult> LoadMatrixInMemory(int xSize, int ySize, List<string> words)
    {
        var result = new MatrixResult();
        var rand = new Random();
        var matrix = InitMatrix(xSize, ySize);

        matrix = FillMatrix(matrix, words, rand);

        matrix = ReplaceEmptyValues(xSize, ySize, matrix, rand);
        result.Matrix = ConvertMatrixToListOfStrings(matrix);

        return result;
    }

    private List<string> ConvertMatrixToListOfStrings(string[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var list = new List<string>();

        for (var i = 0; i < rows; i++)
        {
            var rowString = new StringBuilder();
            for (var j = 0; j < cols; j++) rowString.Append(matrix[i, j]);
            list.Add(rowString.ToString());
        }

        return list;
    }

    private string[,] InitMatrix(int XSize, int YSize)
    {
        var matrix = new string[XSize, YSize];

        for (var i = 0; i < XSize; i++)
        for (var j = 0; j < YSize; j++)
            matrix[i, j] = "";

        return matrix;
    }

    private string[,] ReplaceEmptyValues(int XSize, int YSize, string[,] matrix, Random rand)
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        for (var i = 0; i < matrix.GetLength(0); i++)
        for (var j = 0; j < matrix.GetLength(1); j++)
            if (matrix[i, j] == " " || matrix[i, j] == "")
                matrix[i, j] = alphabet[rand.Next(alphabet.Length)].ToString();

        return matrix;
    }

    private string[,] FillMatrix(string[,] matrix, List<string> words, Random rand)
    {
        words = words.Distinct().ToList();
        foreach (var word in words)
        {
            var occurrences = rand.Next(1, 6); // Random number of occurrences between 1 and 5
            for (var i = 0; i < occurrences; i++) matrix = PlaceWordInMatrix(matrix, word, rand);
        }

        return matrix;
    }

    private string[,] PlaceWordInMatrix(string[,] matrix, string word, Random rand)
    {
        var placed = false;
        var attempts = 0;
        const int maxAttempts = 100; // Limit the number of attempts to place a word
        while (!placed && attempts < maxAttempts)
        {
            attempts++;
            var direction = rand.Next(2); // if random is 0, will go for horizontal, if 1, go for vertical
            int row, col;

            if (direction == 0) // Horizontal
            {
                if (word.Length <= matrix.GetLength(1)) // Change 1
                {
                    row = rand.Next(matrix.GetLength(0));
                    col = rand.Next(matrix.GetLength(1) - word.Length + 1);
                    if (CanPlaceWordHorizontally(matrix, word, row, col))
                    {
                        for (var i = 0; i < word.Length; i++) matrix[row, col + i] = word[i].ToString().ToUpper();

                        placed = true;
                    }
                }
            }
            else // Vertical
            {
                if (word.Length <= matrix.GetLength(0)) // Change 2
                {
                    row = rand.Next(matrix.GetLength(0) - word.Length + 1);
                    col = rand.Next(matrix.GetLength(1));
                    if (CanPlaceWordVertically(matrix, word, row, col))
                    {
                        for (var i = 0; i < word.Length; i++) matrix[row + i, col] = word[i].ToString().ToUpper();

                        placed = true;
                    }
                }
            }
        }

        return matrix;
    }

    private bool CanPlaceWordHorizontally(string[,] matrix, string word, int row, int col)
    {
        for (var i = 0; i < word.Length; i++)
            if (matrix[row, col + i] != "")
                return false;
        return true;
    }

    private bool CanPlaceWordVertically(string[,] matrix, string word, int row, int col)
    {
        for (var i = 0; i < word.Length; i++)
            if (matrix[row + i, col] != "")
                return false;
        return true;
    }

    private void PrintMatrix(string[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++) Console.Write(matrix[i, j] + " ");
            Console.WriteLine();
        }
    }
}