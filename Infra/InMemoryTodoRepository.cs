using LifeTodoConsole.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LifeTodoConsole.Infra
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private List<Todo> todos = new();

        public void Add(Todo todoNew)
        {
            todos.Add(todoNew);
        }

        public List<Todo> GetAll()
        {
            return todos.ToList();
        }

        public void Initialize(IEnumerable<Todo> todos)
        {

            this.todos = todos.ToList();
        }

        public void RemoveAt(int indexRemove)
        {
            todos.RemoveAt(indexRemove);
        }
    }
}