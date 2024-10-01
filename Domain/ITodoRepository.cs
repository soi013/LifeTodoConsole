using System.Collections.Generic;

namespace LifeTodo.Domain
{
    public interface ITodoRepository
    {
        public const int MAX_TODO_COUNT = 5;

        bool IsMaxTodoCount { get; }

        void Add(Todo todoNew);
        List<Todo> GetActiveTodos();
        List<Todo> GetInactiveTodos();
        void Initialize();
        void DoTodo(TodoId id);
        void UpdateTodoExpired();
        void Save();
    }
}