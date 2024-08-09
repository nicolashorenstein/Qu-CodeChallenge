using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.CORE.Interfaces.Helper;

public interface IWordFinder
{
    Task<WordFinderResult> Find(List<string> wordstream);
}