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
    public class Test_RiverController {
        private readonly Mock<IApiCompletion> ApiRepo;
        private readonly Mock<ILogger<RiverController>> MockLogger;
        private readonly RiverController MockOverRiverController;

        public Test_RiverController() {
            MockLogger = new Mock<ILogger<RiverController>>();
            ApiRepo = new Mock<IApiCompletion>();
            MockOverRiverController = new RiverController(ApiRepo.Object, MockLogger.Object);
        }

        [Fact]
        public void Test_CreateRiverTest_ReturnsCreatedAtAction() {
            RiverDTOInput r = new RiverDTOInput();
            RiverDTOutput rOut = new RiverDTOutput() { Name = "testName" };
            ApiRepo.Setup(m => m.AddRiver(r)).Returns(rOut);
            var result = MockOverRiverController.PostRiver(r);
            var temp = result.Result as CreatedAtActionResult;


            Assert.True(result.Result is CreatedAtActionResult);
            Assert.True(temp.Value.Equals(rOut));
        }
        [Fact]
        public void Test_GetRiver_ReturnsCorrectValues() {

            RiverDTOInput r = new RiverDTOInput();
            RiverDTOutput rOut = new RiverDTOutput() { Name = "testName" };
            ApiRepo.Setup(m => m.GetRiverForId(1)).Returns(rOut);
            var result = MockOverRiverController.GetRiver(1);
            var temp = result.Result as OkObjectResult;
            Assert.True(temp.Value.Equals(rOut));
        }

        [Fact]
        public void Test_UpdateRiver_IncorrectId_ReturnsBadRequest() {
            RiverDTOInput r = new RiverDTOInput() { RiverId = 2 };
            RiverDTOutput rOut = new RiverDTOutput() { Name = "testName" };
            ApiRepo.Setup(m => m.UpdateRiver(r)).Returns(rOut);
            var result = MockOverRiverController.PutRiver(1, r);
            Assert.True(result.Result is BadRequestObjectResult);
        }
        [Fact]
        public void TestUpdateRiver_CorrectId_ReturnsCreatedAtAction() {
            RiverDTOInput r = new RiverDTOInput() { RiverId = 1 };
            RiverDTOutput rOut = new RiverDTOutput() { Name = "testName" };
            ApiRepo.Setup(m => m.UpdateRiver(r)).Returns(rOut);
            var result = MockOverRiverController.PutRiver(1, r);
            var temp = result.Result as CreatedAtActionResult;
            Assert.True(result.Result is CreatedAtActionResult);
            Assert.True(temp.Value.Equals(rOut));
        }
        [Fact]
        public void TestDeleteRiver_DomainLayerThrowsException_ReturnsBadRequest() {
            RiverDTOInput r = new RiverDTOInput() { RiverId = 1 };
            RiverDTOutput rOut = new RiverDTOutput() { Name = "testName" };
            ApiRepo.Setup(m => m.DeleteRiver(1)).Throws(new Exception("Test"));
            var result = MockOverRiverController.DeleteRiver(1);
            Assert.True(result is BadRequestResult);
        }

    }
}
