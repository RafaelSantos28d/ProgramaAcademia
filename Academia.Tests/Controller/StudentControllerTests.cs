using Academia.API.Controllers;
using Academia.Application.DTOs.Student;
using Academia.Application.Interfaces;
using Academia.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Tests.Controller
{
    public class StudentControllerTests
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly StudentController _studentController;

        public StudentControllerTests()
        {
            _studentServiceMock = new Mock<IStudentService>();
            _studentController = new StudentController(_studentServiceMock.Object);
        }
        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            //Assert
            var student = new ResponseStudent()
            {
                CPF = "111111111",
                Email = "rafa@gmail.com",
                Name = "Rafa",
                Phone = "997277019"
            };
            var student2 = new ResponseStudent()
            {
                CPF = "111111111",
                Email = "rafa@gmail.com",
                Name = "Rafa",
                Phone = "997277019"
            };
            var resultedExpected = new PagedList<ResponseStudent>(new[] { student, student2 }, 1, 2, 2)
            ;
             _studentServiceMock.Setup(service => service.GetAll(1, 2)).ReturnsAsync(resultedExpected);

            //Act
            var result = await _studentController.GetAll(1,2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<PagedList<ResponseStudent>>(okResult.Value);

            Assert.Equal(resultedExpected, actualValue);
            _studentServiceMock.Verify(s=>s.GetAll(1, 2), Times.Once);

        }

        [Fact]
        public async Task GetById_ShouldReturnOk()
        {
            //Arange

            var expectedResult = new ResponseStudent()
            {
                CPF = "111111111",
                Email = "rafa@gmail.com",
                Name = "Rafa",
                Phone = "997277019"
            };

            _studentServiceMock.Setup(s=>s.GetById(1)).ReturnsAsync(expectedResult);

            //Act
            var result = await _studentController.GetById(1);

            //Assert

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponseStudent>(okResult.Value);

            Assert.Equal(actualValue, expectedResult);
            _studentServiceMock.Verify(s=>s.GetById(1), Times.Once);

        }

        [Fact]
        public async Task Create_ShouldReturnOk()
        {
            //Arrange
            var createStudent = new CreateStudent()
            {
                CPF = "111111111",
                Email = "rafa@gmail.com",
                Name = "Rafa",
                Phone = "997277019"
            };
            var expectedResult = new ResponseStudent()
            {
                StudentId = 1,
                CPF = "111111111",
                Email = "rafa@gmail.com",
                Name = "Rafa",
                Phone = "997277019"
                
            };

            _studentServiceMock.Setup(s=>s.CreateStudent(createStudent)).ReturnsAsync(expectedResult);

            //Act
            var result = await _studentController.Create(createStudent);
            //Assert

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponseStudent>(okResult.Value);
            Assert.Equal(actualValue, expectedResult);
            _studentServiceMock.Verify( s => s.CreateStudent(createStudent),Times.Once);
        }
        [Fact]
        public async Task Remove_ShouldReturnTrue()
        {
            //Arrange
            _studentServiceMock.Setup(s=>s.Remove(1)).ReturnsAsync(true);

            //Act
            var result = await _studentController.Remove(1);

            //Arrange
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(actualValue);
            _studentServiceMock.Verify(s => s.Remove(1), Times.Once);
        }
        [Fact]
        public async Task Update_SholdReturnOk()
        {
            //Arrange
            var updateStudante = new UpdateDTO
            {
                StudentId = 1,
                CPF = "111111111",
                Email = "rafa@gmail.com",
                Name = "Rafa",
                Phone = "997277019"
            };
            var responseStudent = new ResponseStudent
            {
                StudentId = 1,
                CPF = "111111111",
                Email = "rafa@gmail.com",
                Name = "Rafa",
                Phone = "997277019"
            };

            _studentServiceMock.Setup(s=>s.Update(updateStudante)).ReturnsAsync(responseStudent);

            //Act
            var result = await _studentController.Update(1, updateStudante);

            //Assert

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<ResponseStudent>(okResult.Value);
            Assert.Equal(responseStudent, actualValue);

            _studentServiceMock.Verify(s=>s.Update(updateStudante),Times.Once);

        }
    }
}
