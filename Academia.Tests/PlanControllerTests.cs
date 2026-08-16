using Academia.API.Controllers;
using Academia.Application.DTOs.Plan;
using Academia.Application.Interfaces;
using Academia.Application.Services;
using Academia.Domain.Entities;
using Academia.Domain.Pagination;
using Academia.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Academia.Tests
{
    public class PlanControllerTests
    {
        private readonly Mock<IPlanService> _mockService;
        private readonly PlanController _planController;

        public PlanControllerTests()
        {
            _mockService = new Mock<IPlanService>();
            _planController = new PlanController(_mockService.Object);
        }

        [Fact]
        public async Task GetPLans_ShouldReturnOk()
        {
            //Arange

            var plan = new ResponsePlan
            {
                PlanId = 1,
                DurationDays = 30,
                Name = "Test",
                Price = 180
            };
            var plan2 = new ResponsePlan
            {
                PlanId = 2,
                DurationDays = 60,
                Name = "Test",
                Price = 350
            };
            var resultExpected = new List<ResponsePlan>()
            {
                plan,plan2
            };

            _mockService.Setup(service => service.GetAll()).ReturnsAsync(resultExpected);

            //Act
            var result = await _planController.GetAll();

            //Assert
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsAssignableFrom<IEnumerable<ResponsePlan>>(okresult.Value);
            Assert.Equal(resultExpected, actualValue);

            _mockService.Verify(service =>service.GetAll(), Times.Once());
        }

        [Fact]
        public async Task Create_ShouldReturnOK()
        {
            //Arange

            var createPlan = new CreatePlan { DurationDays = 30, Name = "Test" ,Price = 300};

            var resultExpected = new ResponsePlan { PlanId  = 1,Price =300, Name = "Test" ,DurationDays = 30};

            _mockService.Setup(service=> service.Create(createPlan)).ReturnsAsync(resultExpected);

            //Act
            var result = await _planController.Create(createPlan);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponsePlan>(okResult.Value);
            Assert.Equal(resultExpected, actualValue);
            _mockService.Verify(service => service.GetAll(), Times.Once());
        }
        [Fact]
        public async Task GetById_ShouldReturnOK()
        {
            //Range
            var responsePlan = new ResponsePlan { DurationDays = 30, Name = "Test", PlanId = 1, Price = 300 };

            var resultExpected = responsePlan;

            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(responsePlan);

            //Act
            var result = await _planController.GetById(1);

            //Assert

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponsePlan>(okResult.Value);
            Assert.Equal(resultExpected, actualValue);
            _mockService.Verify(service => service.GetById(1), Times.Once());
        }

        [Fact]
        public async Task UpdatePlan_ShouldReturnOk()
        {
            //Arrange

            var planUpdate = new UpdatePlan { DurationDays = 30, PlanId = 1, Name = "Test", Price = 400 };

            var planResponse = new ResponsePlan { DurationDays = 30, PlanId = 1, Name = "Test", Price = 400 };

            var resultExpeted = planResponse;

            _mockService.Setup(service => service.Update(planUpdate)).ReturnsAsync(resultExpeted);

            //Act

            var result = await _planController.Update(1, planUpdate);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponsePlan>(okResult.Value);
            Assert.Equal(actualValue,resultExpeted);
            _mockService.Verify(service => service.Update(planUpdate), Times.Once());


        }
        [Fact]
        public async Task RemovePlan_ShouldReturnTrue()
        {
            //Arange
            _mockService.Setup(x => x.Remove(1)).ReturnsAsync(true);

            //Act
            var result = await _planController.Remove(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var actualValue =Assert.IsType<bool>(okResult.Value);
            Assert.Equal(true,actualValue);

        }
    }
}
