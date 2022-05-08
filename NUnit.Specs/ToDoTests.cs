using Bogus;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnitTestFrameworkComparison;

namespace NUnit.Specs
{
  [TestFixture]
  public class ToDoTests
  {
    private ToDoList _uut;

    [SetUp]
    public void Setup()
    {
      _uut = new ToDoList();
    }

    [Test]
    public void ListIsEmptyOnInit()
    {
      Assert.That(_uut.GetTotalToDoCount, Is.Zero, "ToDo list is not empty");
    }

    [Test]
    public void TotalToDoCountIsUpdatedAfterAdd()
    {
      _uut.Add("Task 1");
      Assert.That(_uut.GetTotalToDoCount, Is.EqualTo(1), "Total todo count is not updated");
    }

    [TestCase("Task 1")]
    [TestCase("Task 2qowejaksdnzklxncmzx")]
    public void CanAddItemOnListWhenUnderCapacity(string item)
    {
      var toDoItemId = _uut.Add(item);

      Assert.That(toDoItemId, Is.Positive, "Failed to add item on list");
    }

    [TestCaseSource(typeof(ToDoItemGenerator), nameof(ToDoItemGenerator.GenerateItems))]
    public void CanAddItemOnListWhenUnderCapacity2(string item)
    {
      var toDoItemId = _uut.Add(item);

      Assert.That(toDoItemId, Is.Positive, "Failed to add item on list");
    }

    [Test]
    public void CannotAddItemOnListWhenOverCapacity()
    {
      var toDoItemId = 0;

      for (var i = 1;toDoItemId >= 0;i++)
      {
        toDoItemId = _uut.Add($"Task {i}");
      }

      Assert.That(toDoItemId, Is.Negative, "Able to add item on list when over capacity");
      Assert.That(_uut.GetTotalToDoCount, Is.EqualTo(3), "ToDo list does not have 3 items");
    }

    [Test]
    public void CannotAddItemOnListIfEmpty()
    {
      var toDoItemId = _uut.Add(" ");

      Assert.That(toDoItemId, Is.Negative, "Able to add invalid item on list");
    }
  }

  public class ToDoItemGenerator
  {
    // Have to use TestCaseData so that VS Test Runner can discover each case correctly
    public static IEnumerable<TestCaseData> GenerateItems() =>
      Enumerable
      .Repeat(0, 2)
      .Select((_, i) => new TestCaseData(new Faker().Random.AlphaNumeric(10)).SetName($"CanAddItemOnListWhenUnderCapacity2(Case {i+1})"));
  }
}