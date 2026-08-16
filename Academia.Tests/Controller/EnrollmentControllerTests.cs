using Academia.API.Controllers;
using Academia.Application.DTOs.Enrollment;
using Academia.Application.Interfaces;
using Academia.Domain.Enums;
using Academia.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Tests.Controller
{
    public class EnrollmentControllerTests
    {
        private readonly Mock<IEnrollmentService> _enrollmentServiceMok;
        private readonly EnrollmentController _enrollmentController;

        public EnrollmentControllerTests()
        {
            _enrollmentServiceMok = new Mock<IEnrollmentService>();
            _enrollmentController = new EnrollmentController(_enrollmentServiceMok.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {

            //Arrange
            var enrollment1 = new ResponseEnrollment()
            {
                EnrollmentId = 1,
                StudentId = 10,
                StdentName = "Rafael",
                PlanId = 5,
                PlanName = "Plano Premium",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1),
                EnrollmentSatatus = EnrollmentSatatus.Active
            };
            var enrollment2 = new ResponseEnrollment()
            {
                EnrollmentId = 2,
                StudentId = 2,
                StdentName = "Pedro",
                PlanId = 5,
                PlanName = "Plano Premium",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1),
                EnrollmentSatatus = EnrollmentSatatus.Active
            };
            var expectedResult = new PagedList<ResponseEnrollment>(new[] { enrollment1, enrollment2 }, 1, 2, 2);

            _enrollmentServiceMok.Setup(s => s.GetAll(1, 2)).ReturnsAsync(expectedResult);

            //Act
            var result = await _enrollmentController.GetAll(1, 2);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<PagedList<ResponseEnrollment>>(okResult.Value);

            Assert.Equal(expectedResult, actualValue);
            _enrollmentServiceMok.Verify(s => s.GetAll(1, 2), Times.Once());

        }
        [Fact]
        public async Task GetById_ShouldReturnOk()
        {
            //Arrange
            var expectedResult = new ResponseEnrollment()
            {
                EnrollmentId = 1,
                StudentId = 10,
                StdentName = "Rafael",
                PlanId = 5,
                PlanName = "Plano Premium",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1),
                EnrollmentSatatus = EnrollmentSatatus.Active
            };
            _enrollmentServiceMok.Setup(s => s.GetById(1)).ReturnsAsync(expectedResult);

            //Act
            var result = await _enrollmentController.GetById(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponseEnrollment>(okResult.Value);

            Assert.Equal(expectedResult, actualValue);
            _enrollmentServiceMok.Verify(s => s.GetById(1), Times.Once());

        }
        [Fact]
        public async Task Create_ShouldReturnOk()
        {
            //Arrange
            var createEnrollment = new CreateEnrollment()
            {

                StudentId = 10,
                PlanId = 5,
                StartDate = new DateTime(2026, 8, 1),
            };
            var expectedResult = new ResponseEnrollment()
            {
                EnrollmentId = 1,
                StudentId = 10,
                StdentName = "Rafael",
                PlanId = 5,
                PlanName = "Plano Premium",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1),
                EnrollmentSatatus = EnrollmentSatatus.Active
            };
            _enrollmentServiceMok.Setup(s => s.CreateEnrollment(createEnrollment)).ReturnsAsync(expectedResult);

            //Act
            var result = await _enrollmentController.Create(createEnrollment);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponseEnrollment>(okResult.Value);

            Assert.Equal(expectedResult, actualValue);
            _enrollmentServiceMok.Verify(s => s.CreateEnrollment(createEnrollment), Times.Once());

        }
        /*
        [Fact]
        public async Task Update_ShouldReturnOk()
        {
            //Arrange
            var expectedResult = new ResponseEnrollment()
            {
                EnrollmentId = 1,
                StudentId = 10,
                StdentName = "Rafael",
                PlanId = 5,
                PlanName = "Plano Premium",
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 9, 1),
                EnrollmentSatatus = EnrollmentSatatus.Active
            };
            var updateEnrollment = new UpdateEnrollment()
            {
                EnrollmentId = 1,
                StudentId = 10,
                PlanId = 5,
                StartDate = new DateTime(2026, 8, 1),
            };
            _enrollmentServiceMok.Setup(s => s.Update(1)).ReturnsAsync(expectedResult);

            //Act
            var result = await _enrollmentController.GetById(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponseEnrollment>(okResult.Value);

            Assert.Equal(expectedResult, actualValue);
            _enrollmentServiceMok.Verify(s => s.GetById(1), Times.Once());

        }*/
        [Fact]
        public async Task Remove_ShouldReturnTrue()
        {
            //Arrange
            _enrollmentServiceMok.Setup(s => s.Cancel(1)).ReturnsAsync(true);

            //Act
            var result = await _enrollmentController.Remove(1);

            //Arrange
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(actualValue);
            _enrollmentServiceMok.Verify(s => s.Cancel(1), Times.Once);
        }
    }
}
