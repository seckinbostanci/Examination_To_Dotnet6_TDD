using IncirAgaci.API.Controllers;
using IncirAgaci.API.Models;
using IncirAgaci.API.Services;
using IncirAgaci.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;


namespace IncirAgaci.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = (OkObjectResult)await sut.Get();

        //Asserts
        result.StatusCode.Should().Be(200);

    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = await sut.Get();

        //Asserts
        mockUserService.Verify(service => service.GetAllUsers(), Times.Once);

    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfUsers()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = await sut.Get();

        //Asserts
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();

    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUserService.Object);

        //Act
        var result = await sut.Get();

        //Asserts
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}