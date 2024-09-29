using LifeTodo.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LifeTodo.Infra
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private List<Todo> todos = new();
        private readonly TodoRepositorySerializer serializer;
        private readonly TodoExpireDomainService todoExpire;

        public InMemoryTodoRepository(TodoRepositorySerializer serializer, TodoExpireDomainService todoExpire)
        {
            this.serializer = serializer;
            this.todoExpire = todoExpire;
        }

        public void Add(Todo todoNew)
        {
            todos.Add(todoNew);
        }

        public List<Todo> GetActiveTodos()
        {
            UpdateTodoExpired();
            return todos
                .Where(x => x.Status == TodoStatus.Active)
                .ToList();
        }

        public List<Todo> GetInactiveTodos()
        {
            UpdateTodoExpired();
            return todos
                .Where(x => x.Status != TodoStatus.Active)
                .ToList();
        }

        public void UpdateTodoExpired()
        {
            foreach (var todo in todos.Where(x => x.Status == TodoStatus.Active))
            {
                todoExpire.ExpireTodoIfNeed(todo);
            }
        }

        public void Initialize()
        {
            this.todos = LoadTodos();
            UpdateTodoExpired();
        }

        public void DoTodo(TodoId targetId)
        {
            UpdateTodoExpired();

            var item = todos.Find(x => x.Id == targetId);
            item.Do();
        }

        private List<Todo> LoadTodos() => serializer.LoadTodos();

        public void Save() => serializer.Save(todos);
    }
}