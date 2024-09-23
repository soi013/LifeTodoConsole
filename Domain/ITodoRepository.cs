using System.Collections.Generic;

namespace LifeTodoConsole.Domain
{
    public interface ITodoRepository
    {
        void Add(Todo todoNew);
        List<Todo> GetAll();
        void Initialize();
        void RemoveAt(int indexRemove);
        void Save();
    }
}