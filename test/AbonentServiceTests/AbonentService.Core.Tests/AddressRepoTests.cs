using AbonentService.Data;
using AbonentService.Data.Models;
using AbonentService.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AbonentService.Core.Tests
{
    public class AddressRepoTests
    {
        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet;
        }

        [Fact]
        public void CreateAddress_should_add_address()
        {
            var addressToCreate = new Address
            {
                AbonentId = 1,
                Name = "testAddress",
                PostalCode = "111222",
                Country = "Serbia",
                City = "Belgrade",
                Street = "Maksima Gorkog",
                Building = "123"
            };
            var mockSet = GetQueryableMockDbSet(new List<Address>());
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);
            var repo = new AddressRepo(mockContext.Object);

            repo.CreateAddress(addressToCreate);
            repo.SaveChanges();

            mockSet.Verify(m => m.Add(It.IsAny<Address>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateAddress_should_return_error_if_null()
        {
            var mockSet = GetQueryableMockDbSet(new List<Address>());
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);
            var repo = new AddressRepo(mockContext.Object);

            var result = () => repo.CreateAddress(null);
            
            result.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void GetAddressById_should_return_address()
        {
            var sampleAddress1 = new Address
            {
                Id = 1,
                AbonentId = 1,
                Name = "testAddress",
                PostalCode = "111222",
                Country = "Serbia",
                City = "Belgrade",
                Street = "Maksima Gorkog",
                Building = "123"
            };
            var sampleAddress2 = new Address
            {
                Id = 2,
                AbonentId = 2,
                Name = "testAddress2",
                PostalCode = "332223",
                Country = "Serbia",
                City = "Belgrade",
                Street = "Maksima Gorkog",
                Building = "421"
            };
            var mockSet = GetQueryableMockDbSet(new List<Address> { sampleAddress1, sampleAddress2 });
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);
            var repo = new AddressRepo(mockContext.Object);

            var result = repo.GetAddressById(2);

            result.Should().BeSameAs(sampleAddress2);
        }

        [Fact]
        public void GetAllAddressesByAbonentId_should_return_addresses_for_abonent()
        {
            var firstAddr = new Address
            {
                AbonentId = 1,
                Name = "testAddress1",
                PostalCode = "111222",
                Country = "Serbia",
                City = "Belgrade",
                Street = "Maksima Gorkog",
                Building = "1"
            };
            var secondAddr = new Address
            {
                AbonentId = 1,
                Name = "testAddress2",
                PostalCode = "111222",
                Country = "Serbia",
                City = "Belgrade",
                Street = "Maksima Gorkog",
                Building = "2"
            };
            var randomAddr = new Address
            {
                AbonentId = 2,
                Name = "testAddress",
                PostalCode = "222333",
                Country = "Serbia",
                City = "Belgrade",
                Street = "Sime Igumanova",
                Building = "123"
            };
            var mockSet = GetQueryableMockDbSet(new List<Address> { firstAddr, secondAddr, randomAddr });
            var mockContext = new Mock<DataContext>();
            mockContext.Setup(m => m.Addresses).Returns(mockSet.Object);
            var repo = new AddressRepo(mockContext.Object);

            var result = repo.GetAllAddressesByAbonentId(1);

            result.Should().HaveCount(2);
            result.Should().Contain(firstAddr);
            result.Should().Contain(secondAddr);
        }
    }
}
