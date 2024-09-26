using LifeTodoConsole.Domain;
using System.Collections.Generic;

namespace LifeTodoConsole.UseCase
{
    public class TodoAppService
    {
        private readonly ITodoRepository todos;

        public TodoAppService(ITodoRepository todos)
        {
            this.todos = todos;
            todos.Initialize();
        }

        public void AddTodo(string? todoTextNew)
        {
            var todoNew = new Todo(todoTextNew!);
            todos.Add(todoNew);
        }

        public List<Todo> GetActiveTodos()
        {
            return todos.GetActiveTodos();
        }

        public List<Todo> GetInactiveTodos()
        {
            return todos.GetInactiveTodos();
        }

        public void DoTodo(Todo item)
        {
            todos.DoTodo(item);
        }

        public void Save()
        {
            todos.Save();
        }
    }
}