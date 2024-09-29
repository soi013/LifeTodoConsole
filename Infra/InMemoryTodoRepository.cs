﻿using LifeTodo.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LifeTodo.Infra
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private List<Todo> todos = new();
        private readonly TodoRepositorySerializer serializer;

        public InMemoryTodoRepository(TodoRepositorySerializer serializer)
        {
            this.serializer = serializer;
        }

        public void Add(Todo todoNew)
        {
            todos.Add(todoNew);
        }

        public List<Todo> GetActiveTodos() => todos
                .Where(x => x.Status == TodoStatus.Active)
                .ToList();
        public List<Todo> GetInactiveTodos() => todos
                .Where(x => x.Status != TodoStatus.Active)
                .ToList();

        public void Initialize()
        {
            this.todos = LoadTodos();
        }

        public void DoTodo(TodoId targetId)
        {
            var item = todos.Find(x => x.Id == targetId);
            item.Status = TodoStatus.Done;
        }

        private List<Todo> LoadTodos() => serializer.LoadTodos();

        public void Save() => serializer.Save(todos);
    }
}