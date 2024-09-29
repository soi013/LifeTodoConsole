using LifeTodo.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LifeTodo.UseCase
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

        public List<TodoDto> GetActiveTodos()
        {
            return todos.GetActiveTodos().Select(t => new TodoDto(t)).ToList();
        }

        public List<TodoDto> GetInactiveTodos()
        {
            return todos.GetInactiveTodos().Select(t => new TodoDto(t)).ToList();

        }

        public void DoTodo(TodoDto item)
        {
            todos.DoTodo(item.Id);
        }

        public void Save()
        {
            todos.Save();
        }
    }
}