using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.Interfaces.Challenge;

public interface IChallengeService
{
    Task<MatrixResult> StartChallenge();
    Task<WordFinderResult> ResolveChallenge(List<string> matrix);
}