using System;
using Microsoft.Extensions.Logging;

namespace Micros.Api.Service.Providers
{
    public class CatsServiceProvider : FunnyServiceProvider, ICatsServiceProvider
    {
        public CatsServiceProvider(ILogger<CatsServiceProvider> logger) : base(logger)
        {
        }

        protected override string Host => Environment.GetEnvironmentVariable("CATS_SERVICE_URL");
    }
}