using GeoServiceAPI.Controllers;
using GeoServiceAPI.Interfaces;
using GeoServiceAPI.Model.Input;
using GeoServiceAPI.Model.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer.ApiTesting.ControllerTesting {
    public class Test_ContinentController {
        private readonly Mock<IApiCompletion> ApiRepo;
        private readonly Mock<ILogger<ContinentController>> MockLogger;
        private readonly ContinentController MockOverContinentController;

        public Test_ContinentController() {
            MockLogger = new Mock<ILogger<ContinentController>>();
            ApiRepo = new Mock<IApiCompletion>();
            MockOverContinentController = new ContinentController(ApiRepo.Object, MockLogger.Object);
        }

        public void Test_CreateContinent_ReturnsCreatedAtAction() {
            ContinentDTOInput c = new ContinentDTOInput();
            ContinentDTOutput co = new ContinentDTOutput();
            ApiRepo.Setup(m => m.AddContinent(c)).Returns(co);
            var r = MockOverContinentController.PostContinent(c);
            Assert.True(r.Result is CreatedAtActionResult);
        }

        [Fact]
        public void Test_CreateContinent_ReturnsIncorrectValues() {
            ContinentDTOInput c = new ContinentDTOInput() { Name = "testName" };
            ContinentDTOutput cOut = new ContinentDTOutput() { Name = c.Name };
            ApiRepo.Setup(m => m.AddContinent(c)).Throws(new Exception(""));
            var result = MockOverContinentController.PostContinent(c);
            Assert.True(cOut.Name == c.Name);
        }

        [Fact]
        public void Test_GetContinentWitCorrectValues() {
            ContinentDTOutput continent = new ContinentDTOutput()
            { ContinentId = "test", Name = "testName", Population = 20 };
            ApiRepo.Setup(m => m.GetContinentForId(1)).Returns(continent);
            var result = MockOverContinentController.GetContinent(1);
            var temp = result.Result as OkObjectResult;
            Assert.True(temp.Value.Equals(continent));
        }

        [Fact]
        public void Test_GetContinent_ValidId() {
            ApiRepo.Setup(m => m.GetContinentForId(1)).Returns(new ContinentDTOutput());
            var result = MockOverContinentController.GetContinent(1);
            Assert.True(result.Result is OkObjectResult);
        }

        [Fact]
        public void Test_UpdateContinent_Invalid() {
            ContinentDTOInput continent = new ContinentDTOInput() { ContinentId = 2 };
            ApiRepo.Setup(m => m.GetContinentForId(1)).Returns(new ContinentDTOutput());
            var result = MockOverContinentController.PutContinent(1, continent);
            Assert.True(result.Result is BadRequestObjectResult);
        }

        [Fact]
        public void Test_DeleteContinent() {
            ApiRepo.Setup(m => m.DeleteContinent(1)).Throws(new Exception("test"));
            var rstly = MockOverContinentController.DeleteContinent(1);
            Assert.True(rstly is BadRequestResult);
        }


        #region Countries
        [Fact]
        public void Test_CreateCountry_ReturnsCorrectObject_CreatedAtAction() {
            CountryDTOInput countryIn = new CountryDTOInput() { ContinentId = 1, Name = "testName" };
            CountryDTOutput countryOut = new CountryDTOutput() { Continent = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.AddCountry(countryIn)).Returns(countryOut);
            var result = MockOverContinentController.PostCountry(1, countryIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.True(temp.Value.Equals(countryOut));
            Assert.True(result.Result is CreatedAtActionResult);
        }
        [Fact]
        public void Test_CreateCountry_InvalidIdS_ReturnsBadRequest() {
            CountryDTOInput countryIn = new CountryDTOInput() { ContinentId = 1, Name = "testName" };
            CountryDTOutput countryOut = new CountryDTOutput() { Continent = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.AddCountry(countryIn)).Returns(countryOut);
            var result = MockOverContinentController.PostCountry(2, countryIn);
            Assert.True(result.Result is BadRequestObjectResult);
        }

        [Fact]
        public void CreateCountry_DomainThrowsException_ReturnsBadRequest() {
            CountryDTOInput countryIn = new CountryDTOInput() { ContinentId = 1, Name = "testName" };
            CountryDTOutput countryOut = new CountryDTOutput() { Continent = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.AddCountry(countryIn)).Throws(new Exception("test"));
            var result = MockOverContinentController.PostCountry(1, countryIn);
            Assert.True(result.Result is BadRequestResult);
        }
        #endregion


        #region cities
        [Fact]
        public void Test_CreateCity_ReturnsCorrectObject_CreatedAtAction() {
            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.AddCity(CityIn)).Returns(CityOut);
            var result = MockOverContinentController.PostCity(1, 2, CityIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.True(temp.Value.Equals(CityOut));
            Assert.True(result.Result is CreatedAtActionResult);
        }

        [Fact]
        public void Test_CreateCity_IdsDoNotMatch_ReturnsBadRequest() {
            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.AddCity(CityIn)).Returns(CityOut);
            var result = MockOverContinentController.PostCity(1, 1, CityIn);
            Assert.True(result.Result is BadRequestObjectResult);
        }

        [Fact]
        public void Test_CreateCity_DomainThrowsException_ReturnsBadRequest() {
            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.AddCity(CityIn)).Throws(new Exception("test"));
            var result = MockOverContinentController.PostCity(1, 1, CityIn);
            Assert.True(result.Result is BadRequestObjectResult);
        }
        [Fact]
        public void Test_GetCity_ReturnsOk_CorrectObject() {
            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.GetCityForId(3)).Returns(CityOut);
            var result = MockOverContinentController.GetCity(1, 2, 3);
            var temp = result.Result as OkObjectResult;
            Assert.True(result.Result is OkObjectResult);
            Assert.True(temp.Value.Equals(CityOut));
        }

        [Fact]
        public void Test_GetCity_DomainThrowsExcpetion_ReturnsBadRequest() {
            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.GetCityForId(3)).Throws(new Exception("test"));
            var result = MockOverContinentController.GetCity(1, 2, 3);
            Assert.True(result.Result is BadRequestResult);
        }

        [Fact]
        public void Test_UpdateCity_IdsDoNotMatch_ReturnsBadRequest() {

            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, CityId = 2, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.UpdateCity(CityIn)).Returns(CityOut);
            var result = MockOverContinentController.PutCity(1, 2, 3, CityIn);
            Assert.True(result.Result is BadRequestObjectResult);
        }
        [Fact]
        public void Test_UpdateCity_ReturnsCreatedAtActionResultWithCorrectValues() {
            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, CityId = 3, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.UpdateCity(CityIn)).Returns(CityOut);
            var result = MockOverContinentController.PutCity(1, 2, 3, CityIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.True(temp.Value.Equals(CityOut));
            Assert.True(result.Result is CreatedAtActionResult);
        }

        [Fact]
        public void Test_UpdateCity_DomainThrowsException_ReturnsNotFound() {
            CityDTOInput CityIn = new CityDTOInput() { ContinentId = 1, CountryId = 2, CityId = 3, Name = "testName" };
            CityDTOutput CityOut = new CityDTOutput() { Country = "1", Name = "testName2" };
            ApiRepo.Setup(x => x.UpdateCity(CityIn)).Throws(new Exception("test"));
            var result = MockOverContinentController.PutCity(1, 2, 3, CityIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.True(result.Result is BadRequestResult);
        }

    }
}
#endregion