using AbonentService.Data;
using AbonentService.Data.Models;
using AbonentService.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;

namespace AbonentService.Core.Tests;

public class AbonentRepoTests
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
    public void CreateAbonent_should_add_abonent_if_not_exist()
    {
        var abonentToCreate = new Abonent
        {
            Name = "A"
        };
        var mockSet = GetQueryableMockDbSet(new List<Abonent>());
        var mockContext = new Mock<DataContext>();
        mockContext.Setup(m => m.Abonents).Returns(mockSet.Object);
        var repo = new AbonentRepo(mockContext.Object);
        
        repo.CreateAbonent(abonentToCreate);
        repo.SaveChanges();

        mockSet.Verify(m => m.Add(It.IsAny<Abonent>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void CreateAbonent_should_return_error_if_exists()
    {
        var abonentToCreate = new Abonent
        {
            Name = "Default"
        };
        var mockSet = GetQueryableMockDbSet(new List<Abonent> { new Abonent { Name = "Default" } });
        var mockContext = new Mock<DataContext>();
        mockContext.Setup(m => m.Abonents).Returns(mockSet.Object);
        var repo = new AbonentRepo(mockContext.Object);

        var result = () => repo.CreateAbonent(abonentToCreate);

        result.Should().ThrowExactly<ArgumentException>();
        mockSet.Verify(m => m.Add(It.IsAny<Abonent>()), Times.Never());
        mockContext.Verify(m => m.SaveChanges(), Times.Never());
    }

    [Fact]
    public void CreateAbonent_should_return_error_if_null()
    {
        var mockSet = GetQueryableMockDbSet(new List<Abonent>());
        var mockContext = new Mock<DataContext>();
        mockContext.Setup(m => m.Abonents).Returns(mockSet.Object);
        var repo = new AbonentRepo(mockContext.Object);

        var result = () => repo.CreateAbonent(null);

        result.Should().ThrowExactly<ArgumentNullException>();
        mockSet.Verify(m => m.Add(It.IsAny<Abonent>()), Times.Never());
        mockContext.Verify(m => m.SaveChanges(), Times.Never());
    }

    [Fact]
    public void GetAbonentById_should_return_abonent()
    {
        var sampleAbonent = new Abonent
        {
            Id = 1,
            Name = "testAbonent1"
        };
        var sampleAbonent2 = new Abonent
        {
            Id = 2,
            Name = "testAbonent2"
        };
        var mockSet = GetQueryableMockDbSet(new List<Abonent> { sampleAbonent, sampleAbonent2 });
        var mockContext = new Mock<DataContext>();
        mockContext.Setup(m => m.Abonents).Returns(mockSet.Object);
        var repo = new AbonentRepo(mockContext.Object);

        var result = repo.GetAbonentById(2);

        result.Should().BeSameAs(sampleAbonent2);
    }

    [Fact]
    public void GetAllAbonents_should_return_all_abonents()
    {
        var sampleAbonent1 = new Abonent
        {
            Id = 1,
            Name = "testAbonent1"
        };
        var sampleAbonent2 = new Abonent
        {
            Id = 2,
            Name = "testAbonent2"
        };
        var mockSet = GetQueryableMockDbSet(new List<Abonent> { sampleAbonent1, sampleAbonent2 });
        var mockContext = new Mock<DataContext>();
        mockContext.Setup(m => m.Abonents).Returns(mockSet.Object);
        var repo = new AbonentRepo(mockContext.Object);

        var result = repo.GetAllAbonents();

        result.Should().HaveCount(2);
        result.Should().Contain(sampleAbonent1);
        result.Should().Contain(sampleAbonent2);
    }
}