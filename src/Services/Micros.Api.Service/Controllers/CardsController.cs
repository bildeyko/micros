using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micros.Api.Service.Services;
using Microsoft.Extensions.Logging;

namespace Micros.Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly ICardsServices _cardsServices;

        public CardsController(ILogger<CardsController> logger, ICardsServices cardsServices)
        {
            _logger = logger;
            _cardsServices = cardsServices;
        }

        [HttpGet]
        public async Task<ActionResult> GetCard()
        {
            await _cardsServices.GenerateRandomCard();

            return new OkResult();
        }
    }
}
