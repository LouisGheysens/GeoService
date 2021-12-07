using GeoServiceAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RiverController : ControllerBase {

        private readonly ILogger Logger;
        private IApiCompletion ApiComplete;

        public RiverController(IApiCompletion api, ILogger<RiverController> logger) {
            ApiComplete = api;
            Logger = logger;
        }

    }
}
