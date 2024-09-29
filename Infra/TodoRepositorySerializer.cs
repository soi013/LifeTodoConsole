using LifeTodo.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LifeTodo.Infra
{
    public class TodoRepositorySerializer
    {
        private readonly IPathSerializeTarget pathTarget;
        private readonly JsonSerializerOptions jsonOptions;

        public TodoRepositorySerializer(IPathSerializeTarget pathTarget)
        {
            this.pathTarget = pathTarget;
            this.jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)       // 日本語表示
            };
        }

        internal List<Todo> LoadTodos()
        {
            List<Todo>? todos = null;
            try
            {
                using var fs = new FileStream(pathTarget.FilePathSerialize, FileMode.Open);
                todos = JsonSerializer.Deserialize<List<Todo>>(fs, jsonOptions);
            }
            catch (Exception)
            {
            }

            return todos ?? new();
        }

        internal void Save(List<Todo> todos)
        {
            var path = Path.GetDirectoryName(pathTarget.FilePathSerialize);
            if (path == null)
            {
                Console.WriteLine("FAILED SAVE TODOs");
                return;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var fs = new FileStream(pathTarget.FilePathSerialize, FileMode.OpenOrCreate);
            JsonSerializer.Serialize(fs, todos, jsonOptions);
        }
    }
}