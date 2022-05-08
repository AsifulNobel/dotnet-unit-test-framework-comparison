using Bogus;
using System.Collections.Generic;
using System.Linq;
using UnitTestFrameworkComparison;
using Xunit;

namespace xUnitNET.Specs
{
  public class ToDoTests
  {
    private readonly ToDoList _uut;

    public ToDoTests()
    {
      _uut = new ToDoList();
    }

    [Fact]
    public void ListIsEmptyOnInit()
    {
      Assert.Equal(0, _uut.GetTotalToDoCount());
    }

    [Fact]
    public void TotalToDoCountIsUpdatedAfterAdd()
    {
      _uut.Add("Task 1");
      Assert.Equal(1, _uut.GetTotalToDoCount());
    }


    [Theory]
    [InlineData("Task 1")]
    [InlineData("Task 2qowejaksdnzklxncmzx")]
    public void CanAddItemOnListWhenUnderCapacity(string item)
    {
      var toDoItemId = _uut.Add(item);

      Assert.True(toDoItemId > 0);
    }

    [Theory]
    [MemberData(nameof(ToDoItemGenerator.GenerateItems), MemberType = typeof(ToDoItemGenerator))]
    public void CanAddItemOnListWhenUnderCapacity2(string item)
    {
      var toDoItemId = _uut.Add(item);

      Assert.True(toDoItemId > 0);
    }

    [Fact]
    public void CannotAddItemOnListWhenOverCapacity()
    {
      var toDoItemId = 0;

      for (var i = 1;toDoItemId >= 0;i++)
      {
        toDoItemId = _uut.Add($"Task {i}");
      }

      Assert.True(toDoItemId < 0);
      Assert.True(_uut.GetTotalToDoCount() == 3);
    }

    [Fact]
    public void CannotAddItemOnListIfEmpty()
    {
      var toDoItemId = _uut.Add(" ");

      Assert.True(toDoItemId < 0);
    }
  }

  public class ToDoItemGenerator
  {
    public static IEnumerable<object[]> GenerateItems() =>
      Enumerable
      .Repeat(0, 2)
      .Select(_ => new object[] { new Faker().Random.AlphaNumeric(10) })
      .ToArray();
  }
}