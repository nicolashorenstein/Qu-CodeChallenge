using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.CORE.Interfaces.Matrix;

public interface IMatrixService
{
    Task<MatrixResult> LoadMatrixInMemory(int XSize, int YSize, List<string> words);
}