using LifeTodoConsole.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LifeTodoConsole.Infra
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private static readonly string FILE_PATH_TODOS = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
          + $"{Path.DirectorySeparatorChar}{nameof(LifeTodoConsole)}{Path.DirectorySeparatorChar}todo.json";

        private List<Todo> todos = new();

        public void Add(Todo todoNew)
        {
            todos.Add(todoNew);
        }

        public List<Todo> GetActiveTodos() => todos
                .Where(x => x.Status == TodoStatus.Active)
                .ToList();
        public List<Todo> GetInactiveTodos() => todos
                .Where(x => x.Status != TodoStatus.Active)
                .ToList();

        public void Initialize()
        {

            this.todos = LoadTodos();
        }

        public void DoneTodoAt(int indexRemove)
        {
            todos[indexRemove].Status = TodoStatus.Done;
        }

        private List<Todo> LoadTodos()
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

        public void Save()
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