using FizzWare.NBuilder;
using ILS.RfqManagement.DomainModel;
using ILS.RfqManagement.DomainModel.Contracts.Repositories;
using ILS.RfqManagement.DomainModel.Requests;
using ILS.RfqManagement.Services;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ILS.RfqManagement.Tests.Services
{
    public class RfqManagementServiceTests
    {
        [Fact]
        public void GetAdministrators_ShouldReturnCorrectValue()
        {
            // Arrange
            var administrators = Builder<AdministratorDetail>.CreateListOfSize(2).Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.GetAdministrators("5024")).Returns(administrators).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.GetAdministrators("5024");

            // Assert
            actual.ShouldBe(administrators);
            repository.Verify();
        }

        [Fact]
        public void AddAdministrators_ShouldAddThenReturnCurrentAdministrators()
        {
            // Arrange
            var request = new AddAdministratorsRequest { SupplierCompanyId = "5024", AdminCompanyIds = new List<string> { "5024U01" } };
            var administrators = Builder<AdministratorDetail>.CreateListOfSize(2).Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.AddAdministrators(request, "auditUser")).Verifiable();
            repository.Setup(r => r.GetAdministrators(request.SupplierCompanyId)).Returns(administrators).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.AddAdministrators(request, "auditUser");

            // Assert
            actual.ShouldBe(administrators);
            repository.Verify();
        }

        [Fact]
        public void RemoveAdministrators_ShouldReturnTrue()
        {
            // Arrange
            var request = new RemoveAdministratorsRequest { AdministratorIds = new List<string> { "some-guid" } };
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.RemoveAdministrators(request, "auditUser")).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.RemoveAdministrators(request, "auditUser");

            // Assert
            actual.ShouldBeTrue();
            repository.Verify();
        }
    }
}
