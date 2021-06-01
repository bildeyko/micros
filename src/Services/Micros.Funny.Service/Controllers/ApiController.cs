using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micros.Funny.Service.Providers;
using Micros.Funny.Service.Services;
using Micros.Funny.Service.ViewModels;
using Microsoft.Extensions.Logging;

namespace Micros.Funny.Service.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IImageService _imageService;
        private readonly IFactService _factService;

        public ApiController(ILogger<ApiController> logger, IImageService imageService,
            IFactService factService)
        {
            _logger = logger;
            _imageService = imageService;
            _factService = factService;
        }

        [HttpGet("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ImageViewModel>> GetImage()
        {
            var url = await _imageService.GetRandomImageUrlAsync();
            return Ok(new ImageViewModel
            {
                ImageUrl = url
            });
        }

        [HttpGet("fact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FactViewModel>> GetFact()
        {
            return Ok(new FactViewModel
            {
                Fact = await _factService.GetRandomFactAsync()
            });
        }
    }
}
