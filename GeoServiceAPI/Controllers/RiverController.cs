using GeoServiceAPI.Interfaces;
using GeoServiceAPI.Model.Input;
using GeoServiceAPI.Model.Output;
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

        #region River
        [HttpGet]
        [Route("{id}")]
        public ActionResult<RiverDTOutput> GetRiver(int id) {
            try {
                Logger.LogInformation("GetRiver called");
                RiverDTOutput result = ApiComplete.GetRiverForId(id);
                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<RiverDTOutput> PostRiver([FromBody] RiverDTOInput rivier) {
            try {
                Logger.LogInformation("PostRiver called");
                RiverDTOutput result = ApiComplete.AddRiver(rivier);
                return CreatedAtAction(nameof(PostRiver), result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<RiverDTOutput> PutRiver(int id, [FromBody] RiverDTOInput river) {
            Logger.LogInformation("PutRiver called");
            if (river == null || river.RiverId != id) {
                return BadRequest("The id for the river was not in good condition");
            }
            else {
                try {
                    return CreatedAtAction(nameof(PutRiver), ApiComplete.UpdateRiver(river));
                }
                catch (Exception ex) {
                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteRiver(int id) {
            try {
                Logger.LogInformation("DeleteRiver called");
                ApiComplete.DeleteRiver(id);
                return Ok("Rivier werd verwijderd!");
            }
            catch (Exception) { return BadRequest(); }
        }

        #endregion

    }
}
