using System.Collections.Generic;

namespace LifeTodoConsole.Domain
{
    public interface ITodoRepository
    {
        void Add(Todo todoNew);
        List<Todo> GetActiveTodos();
        List<Todo> GetInactiveTodos();
        void Initialize();
        void DoneTodoAt(int indexRemove);
        void Save();
    }
}