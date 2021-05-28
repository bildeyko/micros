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
        private readonly IImageProvider _imageProvider;
        private readonly IFactService _factService;

        public ApiController(ILogger<ApiController> logger, IImageProvider imageProvider,
            IFactService factService)
        {
            _logger = logger;
            _imageProvider = imageProvider;
            _factService = factService;
        }

        [HttpGet("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ImageViewModel>> GetImage()
        {
            var url = await _imageProvider.GetImageUrlAsync();
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
