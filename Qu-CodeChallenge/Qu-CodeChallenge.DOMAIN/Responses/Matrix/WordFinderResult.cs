namespace Qu_CodeChallenge.DOMAIN.Responses.Matrix;

public class WordFinderResult : BaseResult
{
    public Dictionary<string, int> Words { get; set; } = new();
}