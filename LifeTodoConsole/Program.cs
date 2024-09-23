using LifeTodoConsole.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LifeTodoConsole
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("LIFE TODO APP");

            var serviceProvider = new AppInstaller().AppProvider;

            var todos = serviceProvider.GetService<ITodoRepository>()!;
            todos.Initialize();

            Console.WriteLine("新しいTODOを入力すると追加されます。消すときは数字を入力してください。何も入力せず、Enterを押すと終了します。");
            Console.WriteLine();

            ShowTodos(todos.GetAll());

            while (true)
            {
                string? todoTextNew = RecieveText();

                if (int.TryParse(todoTextNew, out int indexRemove))
                {
                    todos.RemoveAt(indexRemove);
                }
                else
                {
                    try
                    {
                        var todoNew = new Todo(todoTextNew!, DateTime.Now);
                        todos.Add(todoNew);
                    }
                    catch (ArgumentException)
                    {
                        break;
                    }
                }

                ShowTodos(todos.GetAll());

                Console.WriteLine();
            }

            Console.WriteLine("End App");
            ShowTodos(todos.GetAll());
            todos.Save();
            Console.ReadLine();
        }

        private static string? RecieveText()
        {
            Console.Write("> ");
            string? todoTextNew = Console.ReadLine();
            Console.WriteLine();
            return todoTextNew;
        }

        static void ShowTodos(List<Todo> todos)
        {
            Console.WriteLine($"Current TODOs");
            foreach (var (todo, index) in todos.Select((t, i) => (t, i)))
            {
                Console.WriteLine($" {index}:\t{todo.Text}");
            }
        }
    }
}