using System.Collections.Generic;

namespace LifeTodo.Domain
{
    public interface ITodoRepository
    {
        void Add(Todo todoNew);
        List<Todo> GetActiveTodos();
        List<Todo> GetInactiveTodos();
        void Initialize();
        void DoTodo(TodoId id);
        void UpdateTodoExpired();
        void Save();
    }
}