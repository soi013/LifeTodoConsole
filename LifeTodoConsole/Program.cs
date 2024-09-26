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

            Console.WriteLine("新しいTODOを入力すると追加されます。完了したら数字を入力してください。何も入力せず、Enterを押すと終了します。");
            Console.WriteLine();

            ShowTodos(appService.GetActiveTodos(), appService.GetInactiveTodos());

            while (true)
            {
                string? todoTextNew = RecieveText();

                if (int.TryParse(todoTextNew, out int indexRemove))
                {
                    appService.DoneTodoAt(indexRemove);
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

                ShowTodos(appService.GetActiveTodos(), appService.GetInactiveTodos());

                Console.WriteLine();
            }

            Console.WriteLine("End App");
            ShowTodos(appService.GetActiveTodos(), appService.GetInactiveTodos());
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

        static void ShowTodos(IEnumerable<Todo> activeTodos, IEnumerable<Todo> inactiveTodo)
        {
            Console.WriteLine($"Current TODOs");
            foreach (var (todo, index) in activeTodos.Select((t, i) => (t, i)))
            {
                Console.WriteLine($" {index}:\t{todo.Text}");
            }

            Console.WriteLine($"---INACTIVE---");
            foreach (var (todo, index) in inactiveTodo.Select((t, i) => (t, i)))
            {
                Console.WriteLine($" {index}:\t{todo.Text}");
            }
        }
    }
}