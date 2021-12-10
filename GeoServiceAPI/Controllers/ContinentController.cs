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
    public class ContinentController : ControllerBase {

        private readonly ILogger Logger;
        private IApiCompletion ApiComplete;

        public ContinentController(IApiCompletion api, ILogger<ContinentController> logger) {
            ApiComplete = api;
            Logger = logger;
        }

        #region Continent
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ContinentDTOutput> GetContinent(int id) {
            try {
                Logger.LogInformation("GetContinent called");
                return Ok(ApiComplete.GetContinentForId(id));
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<ContinentDTOutput> PostContinent([FromBody] ContinentDTOInput continent) {
            try {
                Logger.LogInformation("PostContinent called");
                ContinentDTOutput res = ApiComplete.AddContinent(continent);
                return CreatedAtAction(nameof(PostContinent), res);
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ContinentDTOutput> PutContinent(int id, [FromBody] ContinentDTOInput continent) {
            Logger.LogInformation("PutContinent called");
            if (continent.ContinentId != id) {
                return BadRequest("The continentId did not match with the id");
            }
            else {
                return CreatedAtAction(nameof(PutContinent), ApiComplete.UpdateContinent(continent));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteContinent(int id) {
            try {
                Logger.LogInformation("DeleteContinent called");
                ApiComplete.DeleteContinent(id);
                return Ok("Continent werd verwijderd!");
            } catch (Exception) {
                return BadRequest();
            }
        }
        #endregion

        #region Country
        [HttpGet]
        [Route("{id}/Country/{countryId}")]
        public ActionResult<CountryDTOutput> GetCountry(int id, int countryId) {
            try {
                Logger.LogInformation("GetCountry called");
                return Ok(ApiComplete.GetCountryForId(countryId));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("{continentId}/Country")]
        public ActionResult<CountryDTOutput> PostCountry(int continentId, [FromBody] CountryDTOInput country) {
            Logger.LogInformation("PostCountry called");
            if (country.ContinentId != continentId)
                return BadRequest("The continentId did not match");
            else {
                try {
                    CountryDTOutput result = ApiComplete.AddCountry(country);
                    return CreatedAtAction(nameof(PostContinent), result);
                }
                catch (Exception) {
                    return BadRequest();
                }
            }
        }

        [HttpPut]
        [Route("{ContinentId}/Country/{countryId}")]
        public ActionResult<CountryDTOutput> PutCountry(int ContinentId, int countryId, [FromBody] CountryDTOInput country) {
            Logger.LogInformation("PutCountry called");
            if (country.CountryId != countryId) {
                return BadRequest("The countryId's did not match!");
            }
            else {
                try {
                    return CreatedAtAction(nameof(PutCountry), ApiComplete.UpdateCountry(country));
                }
                catch (Exception ex) {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete]
        [Route("{id}/Country/{countryId}")]
        public ActionResult<CountryDTOutput> DeleteCountry(int id, int countryId) {
            try {
                Logger.LogInformation("DeleteCountry called");
                ApiComplete.DeleteCountry(countryId);
                return Ok("Land werd verwijderd!");
            }
            catch (Exception) {
                return BadRequest();
            }
        }
        #endregion

        #region City
        [HttpGet]
        [Route("{id}/country/{countryId}/city/{cityId}")]
        public ActionResult<ContinentDTOutput> GetCity(int id, int countryId, int cityId) {
            try {
                Logger.LogInformation("GetCity called");
                CityDTOutput result = ApiComplete.GetCityForId(cityId);
                return Ok(result);
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("{ContinentId}/Country/{countryId}/City")]
        public ActionResult<ContinentDTOutput> PostCity(int ContinentId, int countryId, [FromBody] CityDTOInput city) {
            Logger.LogInformation("PostCity called");
            if (city.CountryId != countryId) {
                return BadRequest("The countryIds did not match!");
            }
            else {
                try {
                    CityDTOutput result = ApiComplete.AddCity(city);
                    return CreatedAtAction(nameof(PostCity), result);
                }
                catch (Exception) {
                    return BadRequest();
                }
            }
        }

        [HttpPut]
        [Route("{id}/Country/{countryId}/City/{cityId}")]
        public ActionResult<CountryDTOutput> PutCity(int id, int countryId, int cityId, [FromBody] CityDTOInput city) {
            Logger.LogInformation("PutCity called");
            if (city.CityId != cityId) {
                return BadRequest("The cityIds did not match!");
            }
            else {
                try {
                    return CreatedAtAction(nameof(PutCity), ApiComplete.UpdateCity(city));
                }
                catch (Exception) {
                    return BadRequest();
                }
            }
        }

        [HttpDelete]
        [Route("{id}/Country/{countryId}/City/{cityId}")]
        public ActionResult<ContinentDTOutput> DeleteCity(int id, int countryId, int cityId) {
            try {
                Logger.LogInformation("DeleteCity called");
                ApiComplete.DeleteCity(cityId);
                return Ok("City werd verwijderd!");
            }
            catch (Exception) {
                return BadRequest();
            }
        }
        #endregion



    }
}
