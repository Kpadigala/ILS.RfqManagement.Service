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
    }
}
