using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Qu_CodeChallenge.DOMAIN.DTO.Matrix;
using Qu_CodeChallenge.DOMAIN.Queries;
using Qu_CodeChallenge.DOMAIN.Responses;

namespace Qu_CodeChallenge.API.Controllers;

public class QuController : Controller
{
    private readonly IMediator _mediator;

    public QuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("api/qu/loadResources")]
    public async Task<BaseResult> LoadResources([FromBody] LoadResources query)
    {
        return await _mediator.Send(new LoadMatrixQuery
        {
            Words = query.Words,
            XSize = query.XSize,
            YSize = query.YSize
        });
    }
}