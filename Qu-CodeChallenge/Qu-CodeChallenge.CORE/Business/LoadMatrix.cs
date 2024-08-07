using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Qu_CodeChallenge.CORE.Interfaces.Matrix;
using Qu_CodeChallenge.DOMAIN.Queries;
using Qu_CodeChallenge.DOMAIN.Responses.Matrix;

namespace Qu_CodeChallenge.CORE.Business;

public class LoadMatrix
{
    public class ExecuteValidation : AbstractValidator<LoadMatrixQuery>
    {
        public ExecuteValidation()
        {
            RuleFor(query => query.XSize).NotNull();
            RuleFor(query => query.YSize).NotNull();
            RuleFor(query => query.Words).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<LoadMatrixQuery, MatrixResult>
    {
        private readonly IValidator<LoadMatrixQuery> _validator;
        private readonly IMemoryCache _memoryCache;
        private readonly IMatrixService _matrixService;
        
        public Handler(IValidator<LoadMatrixQuery> validator, IMemoryCache memoryCache, IMatrixService matrixService)
        {
            _validator = validator;
            _memoryCache = memoryCache;
            _matrixService = matrixService;
        }
        
        public async Task<MatrixResult> Handle(LoadMatrixQuery request,
            CancellationToken cancellationToken)
        {
            var result = new MatrixResult();
           
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = string.Join(Environment.NewLine, validation.Errors);
                result.SetError(errors, HttpStatusCode.BadRequest);
                return result;
            }

            var matrix = await _matrixService.LoadMatrixInMemory(request.XSize, request.YSize, request.Words);
            
             //_memoryCache.Set("matrixLoaded", matrix);
            
            // // var myObject = new MyClass { Id = 1, Name = "Test Object" };
            //  var aux = "yeahhh"; 
            // _memoryCache.Set("sampleKey", aux);
            //
            //  var cachedObject = _memoryCache.Get<string>("sampleKey");
            //  
            //  return Ok(cachedObject);

            result = matrix;
            return result;
        }
    }
}