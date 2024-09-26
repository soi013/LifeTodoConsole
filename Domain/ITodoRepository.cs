using System.Collections.Generic;

namespace LifeTodoConsole.Domain
{
    public interface ITodoRepository
    {
        void Add(Todo todoNew);
        List<Todo> GetActiveTodos();
        List<Todo> GetInactiveTodos();
        void Initialize();
        void DoTodo(Todo item);
        void Save();
    }
}