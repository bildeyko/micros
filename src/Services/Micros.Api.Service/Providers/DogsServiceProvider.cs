using System;
using Microsoft.Extensions.Logging;

namespace Micros.Api.Service.Providers
{
    public class DogsServiceProvider : FunnyServiceProvider, IDogsServiceProvider
    {
        public DogsServiceProvider(ILogger<DogsServiceProvider> logger) : base(logger)
        {
        }

        protected override string Host => Environment.GetEnvironmentVariable("DOGS_SERVICE_URL");
    }
}