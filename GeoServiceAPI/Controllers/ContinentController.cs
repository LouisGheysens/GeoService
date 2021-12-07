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
    public class ContinentController : ControllerBase {

        private readonly ILogger Logger;
        private IApiCompletion ApiComplete;

        public ContinentController(IApiCompletion api, ILogger<ContinentController> logger) {
            ApiComplete = api;
            Logger = logger;
        }
    }
}
