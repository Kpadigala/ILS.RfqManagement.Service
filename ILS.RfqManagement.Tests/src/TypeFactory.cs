using ILS.RfqManagement.DomainModel;
using ILS.RfqManagement.DomainModel.Contracts.Services;
using Moq;
using System.Collections.Generic;

namespace ILS.RfqManagement.Tests
{
    public static class TypeFactory
    {
        /// <summary>
        /// Creates a mock IRfqManagementService whose GetAdministrators method returns <paramref name="value"/>.
        /// </summary>
        public static Mock<IRfqManagementService> MakeService_GetAdministrators_ReturnValue(IEnumerable<AdministratorDetail> value)
        {
            var mock = new Mock<IRfqManagementService>();
            mock.Setup(s => s.GetAdministrators(It.IsAny<string>()))
                .Returns(value)
                .Verifiable();
            return mock;
        }
    }
}
