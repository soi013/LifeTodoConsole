using LifeTodo.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace LifeTodo.ConsoleApp
{
    class Program
    {
        private static IReadOnlyList<TodoDto> currentActiveTodos = [];
        private static IReadOnlyList<TodoDto> currentInactiveTodos = [];

        static void Main()
        {
            Console.WriteLine("LIFE TODO APP");

            var serviceProvider = new AppInstaller().AppProvider;

            var appService = serviceProvider.GetService<TodoAppService>()!;

            Console.WriteLine("新しいTODOを入力すると追加されます。完了したら数字を入力してください。何も入力せず、Enterを押すと終了します。");
            Console.WriteLine();

            UpdateAndShowTodos(appService.GetActiveTodos(), appService.GetInactiveTodos());

            while (true)
            {
                bool isFinished = Update(appService);
                if (isFinished)
                {
                    break;
                }
            }

            Console.WriteLine("End App");
            UpdateAndShowTodos(appService.GetActiveTodos(), appService.GetInactiveTodos());
            appService.Save();
            Console.ReadLine();
        }

        private static bool Update(TodoAppService appService)
        {
            string? todoTextNew = RecieveText();

            if (int.TryParse(todoTextNew, out int indexDone))
            {
                TodoDto itemDone = currentActiveTodos.ElementAt(indexDone);

                appService.DoTodo(itemDone);
            }
            else
            {
                try
                {
                    appService.AddTodo(todoTextNew);
                }
                catch (ArgumentException)
                {
                    return true;
                }
            }

            UpdateAndShowTodos(appService.GetActiveTodos(), appService.GetInactiveTodos());

            Console.WriteLine();
            return false;
        }

        private static string? RecieveText()
        {
            Console.Write("> ");
            string? todoTextNew = Console.ReadLine();
            Console.WriteLine();
            return todoTextNew;
        }

        static void UpdateAndShowTodos(IEnumerable<TodoDto> activeTodos, IEnumerable<TodoDto> inactiveTodo)
        {
            currentActiveTodos = activeTodos.ToList();
            currentInactiveTodos = inactiveTodo.ToList();

            Console.WriteLine($"Current TODOs");
            foreach (var (todo, index) in currentActiveTodos.Select((t, i) => (t, i)))
            {
                Console.WriteLine($" {index}:\t{todo.Text}");
            }

            Console.WriteLine($"---INACTIVE---");
            foreach (var (todo, index) in currentInactiveTodos.Select((t, i) => (t, i)))
            {
                Console.WriteLine($" {index}:\t{todo.Text}");
            }
        }
    }
}