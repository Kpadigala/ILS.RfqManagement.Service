using FizzWare.NBuilder;
using ILS.RfqManagement.DomainModel;
using ILS.RfqManagement.DomainModel.Contracts.Services;
using ILS.RfqManagement.DomainModel.Requests;
using ILS.RfqManagement.Endpoints.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ILS.RfqManagement.Tests.Endpoints.Controllers
{
    public class RfqManagementControllerTests
    {
        [Fact]
        public void Get_ShouldReturnCorrectValue()
        {
            // Arrange
            var supplierCompanyId = "5024";
            var administrators = Builder<AdministratorDetail>.CreateListOfSize(2)
                .All().With(a => a.SupplierCompanyId = supplierCompanyId)
                .Build();
            var service = TypeFactory.MakeService_GetAdministrators_ReturnValue(administrators);
            var sut = new RfqManagementController(service.Object);

            // Act
            var actual = sut.GetAdministrators(supplierCompanyId);

            // Assert
            actual.Result.ShouldBeNull();
            actual.Value.ShouldBe(administrators);
            service.Verify();
        }

        [Fact]
        public void Get_WithEmptySupplierCompanyId_ShouldReturnBadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.GetAdministrators(string.Empty).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.GetAdministrators(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Add_ShouldReturnCorrectValue()
        {
            // Arrange
            var request = new AddAdministratorsRequest
            {
                SupplierCompanyId = "5024",
                AdminCompanyIds = new List<string> { "5024U01" }
            };
            var administrators = Builder<AdministratorDetail>.CreateListOfSize(1).Build();
            var serviceMock = new Mock<IRfqManagementService>();
            serviceMock.Setup(s => s.AddAdministrators(request, It.IsAny<string>()))
                .Returns(administrators)
                .Verifiable();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.AddAdministrators(request);

            // Assert
            actual.Result.ShouldBeNull();
            actual.Value.ShouldBe(administrators);
            serviceMock.Verify();
        }

        [Fact]
        public void Add_WithEmptySupplierCompanyId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new AddAdministratorsRequest { SupplierCompanyId = string.Empty, AdminCompanyIds = new List<string>() };
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.AddAdministrators(request).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.AddAdministrators(It.IsAny<AddAdministratorsRequest>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Add_WithNullAdminCompanyIds_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new AddAdministratorsRequest { SupplierCompanyId = "5024", AdminCompanyIds = null };
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.AddAdministrators(request).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.AddAdministrators(It.IsAny<AddAdministratorsRequest>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Remove_ShouldReturnCorrectValue()
        {
            // Arrange
            var request = new RemoveAdministratorsRequest { AdministratorIds = new List<string> { "some-guid" } };
            var serviceMock = new Mock<IRfqManagementService>();
            serviceMock.Setup(s => s.RemoveAdministrators(request, It.IsAny<string>()))
                .Returns(true)
                .Verifiable();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.RemoveAdministrators(request);

            // Assert
            actual.Result.ShouldBeNull();
            actual.Value.ShouldBe(true);
            serviceMock.Verify();
        }

        [Fact]
        public void Remove_WithNullAdministratorIds_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new RemoveAdministratorsRequest { AdministratorIds = null };
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.RemoveAdministrators(request).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.RemoveAdministrators(It.IsAny<RemoveAdministratorsRequest>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GetAssignmentRules_ShouldReturnCorrectValue()
        {
            // Arrange
            var supplierCompanyId = "5024";
            var rules = Builder<AssignmentRuleSummary>.CreateListOfSize(2).Build();
            var serviceMock = new Mock<IRfqManagementService>();
            serviceMock.Setup(s => s.GetAssignmentRules(supplierCompanyId)).Returns(rules).Verifiable();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.GetAssignmentRules(supplierCompanyId);

            // Assert
            actual.Result.ShouldBeNull();
            actual.Value.ShouldBe(rules);
            serviceMock.Verify();
        }

        [Fact]
        public void GetAssignmentRules_WithEmptySupplierCompanyId_ShouldReturnBadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.GetAssignmentRules(string.Empty).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.GetAssignmentRules(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GetAssignmentRule_ShouldReturnCorrectValue()
        {
            // Arrange
            var assignmentRuleId = "rule-guid-1";
            var rule = Builder<AssignmentRuleDetail>.CreateNew().Build();
            var serviceMock = new Mock<IRfqManagementService>();
            serviceMock.Setup(s => s.GetAssignmentRule(assignmentRuleId)).Returns(rule).Verifiable();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.GetAssignmentRule(assignmentRuleId);

            // Assert
            actual.Value.ShouldBe(rule);
            serviceMock.Verify();
        }

        [Fact]
        public void GetAssignmentRule_WithEmptyAssignmentRuleId_ShouldReturnBadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.GetAssignmentRule(string.Empty).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.GetAssignmentRule(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GetAssignmentRule_WhenNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var assignmentRuleId = "missing-rule";
            var serviceMock = new Mock<IRfqManagementService>();
            serviceMock.Setup(s => s.GetAssignmentRule(assignmentRuleId)).Returns((AssignmentRuleDetail)null);
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.GetAssignmentRule(assignmentRuleId).Result as NotFoundResult;

            // Assert
            actual.ShouldNotBeNull();
        }

        [Fact]
        public void SaveAssignmentRule_ShouldReturnCorrectValue()
        {
            // Arrange
            var request = new SaveAssignmentRuleRequest
            {
                SupplierCompanyId = "5024",
                AssignToCompanyId = "5030",
                AdministratorId = "admin-guid-1"
            };
            var savedRule = Builder<AssignmentRuleDetail>.CreateNew().Build();
            var serviceMock = new Mock<IRfqManagementService>();
            serviceMock.Setup(s => s.SaveAssignmentRule(request, It.IsAny<string>())).Returns(savedRule).Verifiable();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.SaveAssignmentRule(request);

            // Assert
            actual.Result.ShouldBeNull();
            actual.Value.ShouldBe(savedRule);
            serviceMock.Verify();
        }

        [Fact]
        public void SaveAssignmentRule_WithEmptySupplierCompanyId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new SaveAssignmentRuleRequest { SupplierCompanyId = string.Empty, AssignToCompanyId = "5030", AdministratorId = "admin-guid-1" };
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.SaveAssignmentRule(request).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.SaveAssignmentRule(It.IsAny<SaveAssignmentRuleRequest>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void SaveAssignmentRule_WithEmptyAssignToCompanyId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new SaveAssignmentRuleRequest { SupplierCompanyId = "5024", AssignToCompanyId = string.Empty, AdministratorId = "admin-guid-1" };
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.SaveAssignmentRule(request).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.SaveAssignmentRule(It.IsAny<SaveAssignmentRuleRequest>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void SaveAssignmentRule_WithEmptyAdministratorId_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new SaveAssignmentRuleRequest { SupplierCompanyId = "5024", AssignToCompanyId = "5030", AdministratorId = string.Empty };
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.SaveAssignmentRule(request).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.SaveAssignmentRule(It.IsAny<SaveAssignmentRuleRequest>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void DeleteAssignmentRule_ShouldReturnCorrectValue()
        {
            // Arrange
            var assignmentRuleId = "rule-guid-1";
            var serviceMock = new Mock<IRfqManagementService>();
            serviceMock.Setup(s => s.DeleteAssignmentRule(assignmentRuleId, It.IsAny<string>())).Returns(true).Verifiable();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.DeleteAssignmentRule(assignmentRuleId);

            // Assert
            actual.Result.ShouldBeNull();
            actual.Value.ShouldBe(true);
            serviceMock.Verify();
        }

        [Fact]
        public void DeleteAssignmentRule_WithEmptyAssignmentRuleId_ShouldReturnBadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IRfqManagementService>();
            var sut = new RfqManagementController(serviceMock.Object);

            // Act
            var actual = sut.DeleteAssignmentRule(string.Empty).Result as BadRequestObjectResult;

            // Assert
            actual.ShouldNotBeNull();
            serviceMock.Verify(s => s.DeleteAssignmentRule(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
