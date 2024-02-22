using ASPNETCoreMVCTraining.Models;
using ASPNETCoreMVCTraining.Services;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreMVCTraining.Tests.Services;

[TestFixture]
public class DeptServiceTests
{
    private ApplicationDbContext _applicationDbContext;
    private DeptService _deptService;
    private List<Dept> _depts;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(databaseName: "ASPNETCoreMVCTrainingDb")
          .Options;
        _applicationDbContext = new ApplicationDbContext(options);
        _deptService = new DeptService(_applicationDbContext);
        _depts = [];
    }

    [TearDown]
    public void TearDown()
    {
        if (_depts.Count is not 0)
        {
            _applicationDbContext?.RemoveRange(_depts);
            _applicationDbContext?.SaveChanges();
        }
        _applicationDbContext?.Dispose();
    }

    [TestCase(100)]
    [TestCase(200)]
    public void GetById_WhenDeptNotFound_ReturnNewDept(int id)
    {
        // Arrange
        // Act
        var actual = _deptService.GetById(id);

        // Assert
        Assert.That(actual, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.Null);
            Assert.That(actual.Name, Is.Null);
        });
    }

    [Test]
    public void GetById_WhenDeptExistInDatabase_ReturnDept()
    {
        // Arrange
        var dept = new Dept { Id = 100, Name = "TestDept" };
        _applicationDbContext.Depts.Add(dept);
        _applicationDbContext.SaveChanges();

        _depts.Add(dept);

        // Act
        var actual = _deptService.GetById(100);

        // Assert
        Assert.That(actual, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.EqualTo(100));
            Assert.That(actual.Name, Is.EqualTo("TestDept"));
        });
    }
}
