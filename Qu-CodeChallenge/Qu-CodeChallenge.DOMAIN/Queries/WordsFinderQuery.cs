using MediatR;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.DOMAIN.Queries;

public class WordsFinderQuery : IRequest<WordFinderResult>
{
    public List<string> Matrix { get; set; }
    public List<string> Words { get; set; }
}