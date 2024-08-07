using MediatR;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.DOMAIN.Queries;

public class LoadMatrixQuery : IRequest<MatrixResult>
{
    public int XSize { get; set; }
    public int YSize { get; set; }
    public List<string> Words { get; set; }
}