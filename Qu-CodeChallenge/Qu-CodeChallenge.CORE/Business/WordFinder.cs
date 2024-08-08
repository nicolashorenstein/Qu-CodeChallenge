using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Qu_CodeChallenge.CORE.Interfaces.Matrix;
using Qu_CodeChallenge.DOMAIN.Queries;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.CORE.Business;

public class WordFinder
{
    public class ExecuteValidation : AbstractValidator<WordsFinderQuery>
    {
        public ExecuteValidation()
        {
            RuleFor(query => query.Matrix).NotNull();
            RuleFor(query => query.Words).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<WordsFinderQuery, WordFinderResult>
    {
        private readonly IValidator<WordsFinderQuery> _validator;
        private readonly IMemoryCache _memoryCache;
        
        
        public Handler(IValidator<WordsFinderQuery> validator, IMemoryCache memoryCache, IMatrixService matrixService)
        {
            _validator = validator;
        }
        
        public async Task<WordFinderResult> Handle(WordsFinderQuery request,
            CancellationToken cancellationToken)
        {
            var result = new WordFinderResult();
           
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = string.Join(Environment.NewLine, validation.Errors);
                result.SetError(errors, HttpStatusCode.BadRequest);
                return result;
            }

            var helper = new CORE.Helpers.WordFinder(request.Matrix);
            result = await helper.Find(request.Words);
            
            return result;
        }
    }
}