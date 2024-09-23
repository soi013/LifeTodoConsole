using System.Collections.Generic;

namespace LifeTodoConsole.Domain
{
    public interface ITodoRepository
    {
        void Add(Todo todoNew);
        List<Todo> GetAll();
        void Initialize(IEnumerable<Todo> todos);
        void RemoveAt(int indexRemove);
    }
}