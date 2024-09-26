﻿using FluentAssertions;
using LifeTodoConsole.Domain;
using LifeTodoConsole.Infra;

namespace Test
{
    public class InMemoryTodoRepository_Test
    {
        [Fact]
        public void InMemoryTodoRepository_AddTodo_HaveTodo()
        {
            var rep = new InMemoryTodoRepository();

            //rep.Initialize(); 
            rep.Add(new Todo("test1"));
            rep.Add(new Todo("test2"));

            rep.GetActiveTodos().Should().HaveCount(2);
            rep.GetInactiveTodos().Should().BeEmpty();
            rep.GetActiveTodos().Select(x => x.Text).Should().BeEquivalentTo("test1", "test2");
        }

        [Fact]
        public void InMemoryTodoRepository_DoTodo_HaveInactiveTodo()
        {
            var rep = new InMemoryTodoRepository();

            //rep.Initialize(); 
            rep.Add(new Todo("test1"));
            rep.Add(new Todo("test2"));

            rep.DoneTodoAt(0);

            rep.GetActiveTodos().Should().HaveCount(1);
            rep.GetInactiveTodos().Should().HaveCount(1);
            rep.GetActiveTodos().Select(x => x.Text).Should().BeEquivalentTo("test2");

            rep.DoneTodoAt(0);

            rep.GetActiveTodos().Should().BeEmpty();
            rep.GetInactiveTodos().Should().HaveCount(1);
        }
    }
}
