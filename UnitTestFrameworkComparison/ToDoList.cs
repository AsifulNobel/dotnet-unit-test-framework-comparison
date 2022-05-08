namespace UnitTestFrameworkComparison
{
  public class ToDoList
  {
    private IList<ToDo> _toDos;
    private const int _toDoLimit = 3;
    private int _toDoNextId = 1;

    public ToDoList()
    {
      _toDos = new List<ToDo>(_toDoLimit);
    }

    public int Add(string text)
    {
      var item = new ToDo
      {
        Id = _toDoNextId,
        Text = text
      };

      if (_toDos.Count < _toDoLimit && item.IsValid())
      {
        _toDos.Add(item);
        _toDoNextId++;
        return item.Id;
      }
      return -1;
    }

    public int GetTotalToDoCount() =>
      _toDos.Count;
  }

  public class ToDo
  {
    public int Id { get; init; }
    public string Text { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

    public bool IsValid() => Id > 0 && !string.IsNullOrWhiteSpace(Text);
  }
}
