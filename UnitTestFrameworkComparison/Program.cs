using UnitTestFrameworkComparison;

Console.WriteLine("ToDo Program...");

var app = new ToDoList();

Console.WriteLine($"Total ToDo = {app.GetTotalToDoCount()}");

app.Add("Task 1");
Console.WriteLine($"Total ToDo = {app.GetTotalToDoCount()}");
