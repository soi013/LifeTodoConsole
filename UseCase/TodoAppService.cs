using LifeTodoConsole.Domain;
using System;
using System.Collections.Generic;

namespace LifeTodoConsole.UseCase
{
    public class TodoAppService
    {
        private ITodoRepository todos;

        public TodoAppService(ITodoRepository todos)
        {
            this.todos = todos;
            todos.Initialize();
        }

        public void AddTodo(string? todoTextNew)
        {
            var todoNew = new Todo(todoTextNew!, DateTime.Now);
            todos.Add(todoNew);
        }

        public List<Todo> GetTodos()
        {
            return todos.GetAll();
        }

        public void RemoveTodoAt(int indexRemove)
        {
            todos.RemoveAt(indexRemove);
        }

        public void Save()
        {
            todos.Save();
        }
    }
}