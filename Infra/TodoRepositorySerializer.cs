using LifeTodoConsole.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LifeTodoConsole.Infra
{
    internal class TodoRepositorySerializer
    {
        private static readonly string FILE_PATH_TODOS = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
          + $"{Path.DirectorySeparatorChar}{nameof(LifeTodoConsole)}{Path.DirectorySeparatorChar}todo.json";

        internal List<Todo> LoadTodos()
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

        internal void Save(List<Todo> todos)
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