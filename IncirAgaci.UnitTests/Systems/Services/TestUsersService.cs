using IncirAgaci.API.Config;
using IncirAgaci.API.Models;
using IncirAgaci.API.Services;
using IncirAgaci.UnitTests.Fixtures;
using IncirAgaci.UnitTests.Helpers;
using Microsoft.Extensions.Options;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncirAgaci.UnitTests.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpRequest()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var mockHttpHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHttpHandler.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        await sut.GetAllUsers();

        //Assert
        mockHttpHandler
            .Protected()
            .Verify
            (
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var mockHttpHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHttpHandler.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_NoFoundUsers_Returns404()
    {
        //Arrange
        var mockHttpHandler = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(mockHttpHandler.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpected()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var mockHttpHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHttpHandler.Object);
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();

        //Assert
        result.Count.Should().Be(expectedResponse.Count);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://jsonplaceholder.typicode.com/users";
        var mockHttpHandler = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse,endpoint);
        var httpClient = new HttpClient(mockHttpHandler.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UserService(httpClient, config);

        //Act
        var result = await sut.GetAllUsers();
        var uri = new Uri(endpoint);

        //Assert
        mockHttpHandler
            .Protected()
            .Verify
            (
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get 
                && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
            );
    }
}
