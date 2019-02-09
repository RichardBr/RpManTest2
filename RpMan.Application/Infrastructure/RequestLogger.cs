using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using RpMan.Application.Customers.Queries.GetCustomerDetail;
using RpMan.Application.Customers.Queries.GetCustomersList;

namespace RpMan.Application.Infrastructure
{
    public class RequestPreLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestPreLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            // TODO: Add User Details

            _logger.LogInformation("*** RpMan Request (pre) : {Name} {@Request}", name, request);

            return Task.CompletedTask;
        }
    }

    public class RequestPostLogger<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public RequestPostLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, TResponse response)
        {
            var name = typeof(TRequest).Name;

            // TODO: Add User Details

            _logger.LogInformation("*** RpMan Request (post) : {Name} {@Request}", name, request);

            if (name == typeof(GetCustomersListQuery).Name)
            {
                _logger.LogInformation("*** RpMan Request (post) this is 'GetCustomersListQuery' request.");
            }

            return Task.CompletedTask;
        }
    }

    /*
        // Constrained-Request-Post-Processors, like below, do NOt work yet in aspnet core dependency injection, try others like AUTOFAC, NINJECT

        public class ConstrainedRequestPostLogger<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
            where TRequest : GetCustomersListQuery
        {
            private readonly ILogger _logger;

            public ConstrainedRequestPostLogger(ILogger<TRequest> logger)
            {
                _logger = logger;
            }

            public Task Process(TRequest request, TResponse response)
            {
                var name = typeof(TRequest).Name;

                _logger.LogInformation("*** RpMan Request (constrained post for GetCustomersListQuery) : {Name} {@Request}", name, request);

                return Task.CompletedTask;
            }
        }
    */

}
