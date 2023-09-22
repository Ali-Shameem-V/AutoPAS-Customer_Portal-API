using angularAPI.Controllers;
using angularAPI.Model.Dto;
using angularAPI.Services.Interface;
using AutoFixture;
using AutoPortal.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
            var policyNumber = _fixture.Create<IEnumerable<int>>();
            _policyServiceMock.Setup(s => s.getPolicyNumber()).ReturnsAsync(policyNumber);

            // Act
            var result = await _controller.GetPolicyNumber();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(policyNumber);
        }

        [Fact]
        public async Task GetPolicyNumber_ReturnsOkWithMessage_WhenPolicyNumberIsNull()
        {
            // Arrange
            _policyServiceMock.Setup(s => s.getPolicyNumber()).ReturnsAsync((IEnumerable<int>)null);

            // Act
            var result = await _controller.GetPolicyNumber();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(new { message = "Empty" });
        }

        [Fact]
        public async Task GetPolicyNumber_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _policyServiceMock.Setup(s => s.getPolicyNumber()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetPolicyNumber();

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Subject.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task GetPolicyDetailsByPolicyNumber_ReturnsOk_WhenPolicyDetailsFound()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            var policyDetails = new PolicyDetailsDto { /* Initialize with some data */ };
            _policyServiceMock.Setup(s => s.getPolicyDetailsByPolicyNumber(policyNumber)).ReturnsAsync(policyDetails);

            // Act
            var result = await _controller.GetPolicyDetailsByPolicyNumber(policyNumber);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(policyDetails);
        }

        [Fact]
        public async Task GetPolicyDetailsByPolicyNumber_ReturnsNotFound_WhenPolicyDetailsNotFound()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.getPolicyDetailsByPolicyNumber(policyNumber)).ReturnsAsync((PolicyDetailsDto)null);

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
            _policyServiceMock.Setup(s => s.getPolicyDetailsByPolicyNumber(policyNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetPolicyDetailsByPolicyNumber(policyNumber);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Subject.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task GetAll_ReturnsOk_WhenPortalUserIsNotNull()
        {
            // Arrange
            var portalUser = _fixture.Create<portaluser>();
            _policyServiceMock.Setup(s => s.getId()).ReturnsAsync(portalUser);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(portalUser);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenPortalUserIsNull()
        {
            // Arrange
            _policyServiceMock.Setup(s => s.getId()).ReturnsAsync((portaluser)null);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAll_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _policyServiceMock.Setup(s => s.getId()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Subject.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsOkWithFalse_WhenValidationFails()
        {
            // Arrange
            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.validatePolicyChassisNumber(policyDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(false);
        }

        [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsOkWithTrue_WhenValidationSucceeds()
        {
            // Arrange
            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.validatePolicyChassisNumber(policyDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task ValidateChassisPolicyNumber_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var policyDto = _fixture.Create<PolicyDto>();
            _policyServiceMock.Setup(s => s.validatePolicyChassisNumber(policyDto)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.ValidateChassisPolicyNumber(policyDto);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Subject.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task AddUserPolicyDetails_ReturnsOk_WhenUserPolicyDetailsAdded()
        {
            // Arrange
            var policyListDto = _fixture.Create<PolicyListDto>();
            _policyServiceMock.Setup(s => s.addUserPolicyDetails(policyListDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddUserPolicyDetails(policyListDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task AddUserPolicyDetails_ReturnsBadRequest_WhenUserPolicyDetailsNotAdded()
        {
            // Arrange
            var policyListDto = _fixture.Create<PolicyListDto>();
            _policyServiceMock.Setup(s => s.addUserPolicyDetails(policyListDto)).ReturnsAsync(false);

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
            _policyServiceMock.Setup(s => s.addUserPolicyDetails(policyListDto)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.AddUserPolicyDetails(policyListDto);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Subject.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task RemovePolicyDetails_ReturnsOk_WhenPolicyDetailsRemoved()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.removePolicyDetails(policyNumber)).ReturnsAsync(true);

            // Act
            var result = await _controller.RemovePolicyDetails(policyNumber);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task RemovePolicyDetails_ReturnsOkWithFalse_WhenPolicyDetailsNotFound()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.removePolicyDetails(policyNumber)).ReturnsAsync(false);

            // Act
            var result = await _controller.RemovePolicyDetails(policyNumber);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(false);
        }

        [Fact]
        public async Task RemovePolicyDetails_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            _policyServiceMock.Setup(s => s.removePolicyDetails(policyNumber)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.RemovePolicyDetails(policyNumber);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Subject.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task AddUser_ReturnsOk_WhenUserAdded()
        {
            // Arrange
            var portalUser = _fixture.Create<portaluser>();
            _policyServiceMock.Setup(s => s.login(portalUser)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddUser(portalUser);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task AddUser_ReturnsOkWithFalse_WhenUserNotAdded()
        {
            // Arrange
            var portalUser = _fixture.Create<portaluser>();
            _policyServiceMock.Setup(s => s.login(portalUser)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddUser(portalUser);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(false);
        }

        [Fact]
        public async Task AddUser_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var portalUser = _fixture.Create<portaluser>();
            _policyServiceMock.Setup(s => s.login(portalUser)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.AddUser(portalUser);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Subject.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}



