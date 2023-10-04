using angularAPI.Controllers;
using angularAPI.Model.Dto;
using angularAPI.Services.Interface;
using AutoFixture;
using AutoPortal.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace AutoPasTest.Controller
{
    public class PolicyControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IPolicy> _policyServiceMock;
        private readonly PolicyController _controller;
        public PolicyControllerTest()
        {
            _fixture = new Fixture();
            _policyServiceMock = _fixture.Freeze<Mock<IPolicy>>();
            _fixture.Customize<portaluser>(c => c.Without(t => t.userpolicylists));
            _controller = new PolicyController(_policyServiceMock.Object);

        }
        [Fact]
        public async Task GetPolicyNumber_ReturnsOk_WhenPolicyNumberIsNotNull()
        {
            // Arrange
            var username = _fixture.Create<string>();
            var policyNumber = _fixture.CreateMany<int>().ToList();
            _policyServiceMock.Setup(s => s.GetPolicyNumberByUserId(username)).ReturnsAsync((List<int>)policyNumber);

            // Act
            var result = await _controller.GetPolicyNumber(username);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().Be(policyNumber);
        }

        [Fact]
        public async Task GetPolicyNumber_ReturnsOkWithNull_WhenPolicyNumberIsNull()
        {
            // Arrange
            var username = _fixture.Create<string>();
            _policyServiceMock.Setup(s => s.GetPolicyNumberByUserId(username)).ReturnsAsync((List<int>)null);

            // Act
            var result = await _controller.GetPolicyNumber(username);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().BeNull();
        }

        [Fact]
        public async Task GetPolicyNumber_ReturnsBadRequest_OnException()
        {
            // Arrange
            var username = _fixture.Create<string>();

            _policyServiceMock.Setup(s => s.GetPolicyNumberByUserId(username)).ThrowsAsync(new Exception("An Error Occured"));

            // Act
            var result = await _controller.GetPolicyNumber(username);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value
                               .Should().BeEquivalentTo("An Error Occured");
        }
    
   
        [Fact]
        public async Task GetPolicyDetailsByPolicyNumber_ReturnsOk_WhenPolicyDetailsFound()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            var policyDetails = new PolicyDetailsDto {};
            _policyServiceMock.Setup(s => s.GetPolicyDetailsByPolicyNumber(policyNumber)).ReturnsAsync(policyDetails);

            // Act
            var result = await _controller.GetPolicyDetailsByPolicyNumber(policyNumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().BeEquivalentTo(policyDetails);
        }

        [Fact]
        public async Task GetPolicyDetailsByPolicyNumber_ReturnsNotFound_WhenPolicyDetailsNotFound()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.GetPolicyDetailsByPolicyNumber(policyNumber)).ReturnsAsync((PolicyDetailsDto)null);

            // Act
            var result = await _controller.GetPolicyDetailsByPolicyNumber(policyNumber);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetPolicyDetailsByPolicyNumber_ReturnsInternalServerError_OnException()
        {
            // Arrange


            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.GetPolicyDetailsByPolicyNumber(policyNumber)).ThrowsAsync(new Exception("An Error Occured"));

            // Act
            var result = await _controller.GetPolicyDetailsByPolicyNumber(policyNumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value
                            .Should().BeEquivalentTo("An Error Occured");
        }
       
       [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsOkWithInvalidChassisMessage()
        {
            // Arrange
            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.ValidatePolicyChassisNumber(policyDto)).ReturnsAsync("Invalid Chassis Number");

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject
            .Value.Should().BeEquivalentTo(new { message = "Invalid Chassis" });
        }

        [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsOkWithInvalidPolicyMessage()
        {
            // Arrange
            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.ValidatePolicyChassisNumber(policyDto)).ReturnsAsync("Invalid Policy Number");

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject
            .Value.Should().BeEquivalentTo(new { message = "Invalid Policy" });
        }

        [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsOkWithInvalidPolicyAndChassisMessage()
        {
            // Arrange
            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.ValidatePolicyChassisNumber(policyDto)).ReturnsAsync("Invalid Policy & Chassis Number");

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().BeEquivalentTo(new { message = "Invalid Policy & Chassis" });
        }

        [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsOkWithValidMessage()
        {
            // Arrange
            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.ValidatePolicyChassisNumber(policyDto)).ReturnsAsync("Valid");

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().BeEquivalentTo(new { message = "Valid" });
        }

        [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsInternalServerError_OnException()
        {
            // Arrange

            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.ValidatePolicyChassisNumber(policyDto)).ThrowsAsync(new Exception("An Error Occured"));

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value
                .Should().BeEquivalentTo("An Error Occured");
        }


        
        [Fact]
        public async Task AddUserPolicyDetails_ReturnsOk_WhenUserPolicyDetailsAdded()
        {
            // Arrange
            var policyListDto = _fixture.Create<PolicyListDto>();
            _policyServiceMock.Setup(s => s.AddUserPolicyDetails(policyListDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddUserPolicyDetails(policyListDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
            .Should().BeEquivalentTo(true);
        }

        [Fact]
        public async Task AddUserPolicyDetails_ReturnsBadRequest_WhenUserPolicyDetailsNotAdded()
        {
            // Arrange
            var policyListDto = _fixture.Create<PolicyListDto>();
            _policyServiceMock.Setup(s => s.AddUserPolicyDetails(policyListDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddUserPolicyDetails(policyListDto);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task AddUserPolicyDetails_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var policyListDto = _fixture.Create<PolicyListDto>();

            _policyServiceMock.Setup(s => s.AddUserPolicyDetails(policyListDto)).ThrowsAsync(new Exception("An Error Occured"));

            // Act
            var result = await _controller.AddUserPolicyDetails(policyListDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value
                .Should().BeEquivalentTo("An Error Occured");
        }
        [Fact]
        public async Task RemovePolicyDetails_ReturnsOk_WhenPolicyDetailsRemoved()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.RemovePolicyDetails(policyNumber)).ReturnsAsync(true);

            // Act
            var result = await _controller.RemovePolicyDetails(policyNumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().Be(true);
        }

        [Fact]
        public async Task RemovePolicyDetails_ReturnsOkWithFalse_WhenPolicyDetailsNotFound()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.RemovePolicyDetails(policyNumber)).ReturnsAsync(false);

            // Act
            var result = await _controller.RemovePolicyDetails(policyNumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().Be(false);
        }

        [Fact]
        public async Task RemovePolicyDetails_ReturnsInternalServerError_OnException()
        {
            // Arrange


            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.RemovePolicyDetails(policyNumber)).ThrowsAsync(new Exception("An Error Occured"));

            // Act
            var result = await _controller.RemovePolicyDetails(policyNumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value
                            .Should().BeEquivalentTo("An Error Occured");
        }
        [Fact]
        public async Task AddUser_ReturnsOk_WhenUserAdded()
        {
            // Arrange
            var portalUser = _fixture.Create<portaluser>();
            _policyServiceMock.Setup(s => s.Login(portalUser)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddUser(portalUser);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().Be(true);
        }

        [Fact]
        public async Task AddUser_ReturnsOkWithFalse_WhenUserNotAdded()
        {
            // Arrange
            var portalUser = _fixture.Create<portaluser>();
            _policyServiceMock.Setup(s => s.Login(portalUser)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddUser(portalUser);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                .Should().Be(false);
        }

        [Fact]
        public async Task AddUser_ReturnsInternalServerError_OnException()
        {
            // Arrange


            var portalUser = _fixture.Create<portaluser>();
            _policyServiceMock.Setup(s => s.Login(portalUser)).ThrowsAsync(new Exception("An Error Occured"));

            // Act
            var result = await _controller.AddUser(portalUser);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Subject.Value
                 .Should().BeEquivalentTo("An Error Occured");
        }
    }
}



