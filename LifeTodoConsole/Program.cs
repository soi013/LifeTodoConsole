using System.Text.Json;

namespace LifeTodoConsole
{
    class Program
    {
        private static readonly string FILE_PATH_TODOS = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            + $"{Path.DirectorySeparatorChar}{nameof(LifeTodoConsole)}{Path.DirectorySeparatorChar}todo.json";

        static void Main()
        {
            Console.WriteLine("LIFE TODO APP");

            List<Todo> todos = LoadTodos();

            Console.WriteLine("新しいTODOを入力すると追加されます。消すときは数字を入力してください。何も入力せず、Enterを押すと終了します。");
            Console.WriteLine();

            ShowTodos(todos);

            while (true)
            {
                string? todoTextNew = RecieveText();

                if (string.IsNullOrWhiteSpace(todoTextNew))
                {
                    break;
                }
                else if (int.TryParse(todoTextNew, out int indexRemove))
                {
                    todos.RemoveAt(indexRemove);
                }
                else
                {
                    var todoNew = new Todo(todoTextNew!, DateTime.Now);
                    todos.Add(todoNew);

                }

                ShowTodos(todos);

                Console.WriteLine();
            }

            Console.WriteLine("End App");
            ShowTodos(todos);
            SaveTodos(todos);
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

        private static List<Todo> LoadTodos()
        {
            List<Todo>? todos = null;
            try
            {
                using var fs = new FileStream(FILE_PATH_TODOS, FileMode.Open);
                todos = JsonSerializer.Deserialize<List<Todo>>(fs);
            }
            catch (Exception)
            {
            }

            return todos ?? new();
        }

        private static void SaveTodos(List<Todo> todos)
        {
            var path = Path.GetDirectoryName(FILE_PATH_TODOS);
            if (path == null)
            {
                Console.WriteLine("FAILED SAVE TODOs");
                return;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var fs = new FileStream(FILE_PATH_TODOS, FileMode.OpenOrCreate);
            JsonSerializer.Serialize(fs, todos);
        }
    }
}