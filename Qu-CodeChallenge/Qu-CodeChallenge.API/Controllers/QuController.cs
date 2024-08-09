using MediatR;
using Microsoft.AspNetCore.Mvc;
using Qu_CodeChallenge.DOMAIN.DTO.Matrix;
using Qu_CodeChallenge.DOMAIN.Queries;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.API.Controllers;

public class QuController : Controller
{
    private readonly IMediator _mediator;

    public QuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/qu/loadResources")]
    public async Task<MatrixResult> LoadResources([FromBody] LoadResources query)
    {
        var result = await _mediator.Send(new LoadMatrixQuery
        {
            Words = query.Words,
            XSize = query.XSize,
            YSize = query.YSize
        });

        return result;
    }

    [HttpPost("api/qu/resolve")]
    public async Task<WordFinderResult> Resolve([FromBody] ResolveChallenge query)
    {
        var result = await _mediator.Send(new WordsFinderQuery
        {
            Words = query.Words,
            Matrix = query.Matrix
        });

        return result;
    }
}