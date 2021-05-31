using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micros.Image.Generator.Service.Services;
using Micros.Image.Generator.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Micros.Image.Generator.Service.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGeneratorService _generatorService;

        public ApiController(ILogger<WeatherForecastController> logger, IGeneratorService generatorService)
        {
            _logger = logger;
            _generatorService = generatorService;
        }

        [HttpPost]
        public async Task<ActionResult<GenerationResult>> Generate(GenerationTask task)
        {
            var fileKey = await _generatorService.GenerateByImageUrlAsync(task.ImageUrl, task.Title);

            return Ok(new GenerationResult
            {
                Status = true,
                FileKey = fileKey
            });
        }
    }
}
