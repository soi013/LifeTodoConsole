using LifeTodoConsole.Domain;
using LifeTodoConsole.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace LifeTodoConsole
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("LIFE TODO APP");

            var serviceProvider = new AppInstaller().AppProvider;

            var appService = serviceProvider.GetService<TodoAppService>()!;

            Console.WriteLine("新しいTODOを入力すると追加されます。消すときは数字を入力してください。何も入力せず、Enterを押すと終了します。");
            Console.WriteLine();

            ShowTodos(appService.GetTodos());

            while (true)
            {
                string? todoTextNew = RecieveText();

                if (int.TryParse(todoTextNew, out int indexRemove))
                {
                    appService.RemoveTodoAt(indexRemove);
                }
                else
                {
                    try
                    {
                        appService.AddTodo(todoTextNew);
                    }
                    catch (ArgumentException)
                    {
                        break;
                    }
                }

                ShowTodos(appService.GetTodos());

                Console.WriteLine();
            }

            Console.WriteLine("End App");
            ShowTodos(appService.GetTodos());
            appService.Save();
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