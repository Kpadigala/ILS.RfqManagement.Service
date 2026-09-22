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

        [Fact]
        public void GetAssignmentRules_ShouldReturnCorrectValue()
        {
            // Arrange
            var rules = Builder<AssignmentRuleSummary>.CreateListOfSize(2).Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.GetAssignmentRules("5024")).Returns(rules).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.GetAssignmentRules("5024");

            // Assert
            actual.ShouldBe(rules);
            repository.Verify();
        }

        [Fact]
        public void GetAssignmentRule_ShouldReturnCorrectValue()
        {
            // Arrange
            var rule = Builder<AssignmentRuleDetail>.CreateNew().Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.GetAssignmentRule("rule-guid-1")).Returns(rule).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.GetAssignmentRule("rule-guid-1");

            // Assert
            actual.ShouldBe(rule);
            repository.Verify();
        }

        [Fact]
        public void SaveAssignmentRule_ShouldSaveThenReturnTheSavedRule()
        {
            // Arrange
            var request = new SaveAssignmentRuleRequest { SupplierCompanyId = "5024", AssignToCompanyId = "5030", AdministratorId = "admin-guid-1" };
            var savedRule = Builder<AssignmentRuleDetail>.CreateNew().Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.SaveAssignmentRule(request, "auditUser")).Returns("rule-guid-1").Verifiable();
            repository.Setup(r => r.GetAssignmentRule("rule-guid-1")).Returns(savedRule).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.SaveAssignmentRule(request, "auditUser");

            // Assert
            actual.ShouldBe(savedRule);
            repository.Verify();
        }

        [Fact]
        public void DeleteAssignmentRule_ShouldReturnTrue()
        {
            // Arrange
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.DeleteAssignmentRule("rule-guid-1", "auditUser")).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.DeleteAssignmentRule("rule-guid-1", "auditUser");

            // Assert
            actual.ShouldBeTrue();
            repository.Verify();
        }

        [Fact]
        public void GetManualAssignments_ShouldReturnCorrectValue()
        {
            // Arrange
            var assignments = Builder<ManualAssignment>.CreateListOfSize(2).Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.GetManualAssignments("rfq-guid-1")).Returns(assignments).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.GetManualAssignments("rfq-guid-1");

            // Assert
            actual.ShouldBe(assignments);
            repository.Verify();
        }

        [Fact]
        public void InsManualAssignments_ShouldInsertThenReturnTheCurrentManualAssignments()
        {
            // Arrange
            var request = new InsManualAssignmentsRequest
            {
                RfqId = "rfq-guid-1",
                SupplierCompanyId = "5024",
                AssignToCompanyIds = new List<string> { "5030" },
                AdministratorId = "admin-guid-1"
            };
            var assignments = Builder<ManualAssignment>.CreateListOfSize(1).Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.InsManualAssignments(request, "auditUser")).Verifiable();
            repository.Setup(r => r.GetManualAssignments("rfq-guid-1")).Returns(assignments).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.InsManualAssignments(request, "auditUser");

            // Assert
            actual.ShouldBe(assignments);
            repository.Verify();
        }

        [Fact]
        public void DeleteManualAssignments_ShouldDeleteThenReturnTheCurrentManualAssignments()
        {
            // Arrange
            var request = new DeleteManualAssignmentsRequest
            {
                RfqId = "rfq-guid-1",
                SupplierCompanyId = "5024",
                AssignToCompanyIds = new List<string> { "5030" }
            };
            var assignments = new List<ManualAssignment>();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.DeleteManualAssignments(request, "auditUser")).Verifiable();
            repository.Setup(r => r.GetManualAssignments("rfq-guid-1")).Returns(assignments).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.DeleteManualAssignments(request, "auditUser");

            // Assert
            actual.ShouldBe(assignments);
            repository.Verify();
        }

        [Fact]
        public void GetAssignedAdministrators_ShouldReturnCorrectValue()
        {
            // Arrange
            var administratorIds = new List<string> { "admin-guid-1", "admin-guid-2" };
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.GetAssignedAdministrators("5024", "rfq-guid-1")).Returns(administratorIds).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.GetAssignedAdministrators("5024", "rfq-guid-1");

            // Assert
            actual.ShouldBe(administratorIds);
            repository.Verify();
        }

        [Fact]
        public void MatchNewRfqToAssignmentRules_ShouldReturnTrue()
        {
            // Arrange
            var request = new MatchNewRfqToAssignmentRulesRequest
            {
                RfqId = "rfq-guid-1",
                SupplierCompanyId = "5024",
                BuyerCompanyId = "9001",
                PartNumbers = new List<string> { "A100" }
            };
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.MatchNewRfqToAssignmentRules(request, "auditUser")).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.MatchNewRfqToAssignmentRules(request, "auditUser");

            // Assert
            actual.ShouldBeTrue();
            repository.Verify();
        }

        [Fact]
        public void GetPendingRfqMatches_ShouldReturnCorrectValue()
        {
            // Arrange
            var pendingMatches = Builder<PendingRfqMatch>.CreateListOfSize(2).Build();
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.GetPendingRfqMatches()).Returns(pendingMatches).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.GetPendingRfqMatches();

            // Assert
            actual.ShouldBe(pendingMatches);
            repository.Verify();
        }

        [Fact]
        public void ClearPendingRfqMatch_ShouldReturnTrue()
        {
            // Arrange
            var repository = new Mock<IRfqManagementRepository>();
            repository.Setup(r => r.ClearPendingRfqMatch("rfq-guid-1")).Verifiable();
            var sut = new RfqManagementService(repository.Object);

            // Act
            var actual = sut.ClearPendingRfqMatch("rfq-guid-1");

            // Assert
            actual.ShouldBeTrue();
            repository.Verify();
        }
    }
}
