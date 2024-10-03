using FluentAssertions;
using LifeTodo.Domain;
using LifeTodo.Infra;
using Test.TestSource;

namespace Test
{
    public class InMemoryTodoRepository_Test
    {
        public InMemoryTodoRepository_Test()
        {
            CleanTestDirectory();
        }

        private static void CleanTestDirectory()
        {
            var path = new PathTemporary();
            if (Directory.Exists(path.FilePathSerialize))
            {
                Directory.Delete(path.FilePathSerialize, true);
            }
        }

        [Fact]
        public void InMemoryTodoRepository_AddTodo_HaveTodo()
        {
            var path = new PathTemporary();
            var serializer = new TodoRepositorySerializer(path);
            var rep = new InMemoryTodoRepository(serializer, new());

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
            IPathSerializeTarget path = new PathTemporary();
            var serializer = new TodoRepositorySerializer(path);
            var rep = new InMemoryTodoRepository(serializer, new());

            //rep.Initialize(); 
            rep.Add(new Todo("test1"));
            rep.Add(new Todo("test2"));

            var todoDone = rep.GetActiveTodos()[0];
            rep.DoTodo(todoDone.Id);

            rep.GetActiveTodos().Should().HaveCount(1);
            rep.GetInactiveTodos().Should().HaveCount(1);
            rep.GetActiveTodos().Select(x => x.Text).Should().BeEquivalentTo("test2");

            todoDone = rep.GetActiveTodos()[0];
            rep.DoTodo(todoDone.Id);

            rep.GetActiveTodos().Should().BeEmpty();
            rep.GetInactiveTodos().Should().HaveCount(2);
        }

        [Fact]
        public void InMemoryTodoRepository_DoTodo_NotChangeDoTodo()
        {
            IPathSerializeTarget path = new PathTemporary();
            var serializer = new TodoRepositorySerializer(path);
            var rep = new InMemoryTodoRepository(serializer, new());

            //rep.Initialize(); 
            rep.Add(new Todo("test1"));
            rep.Add(new Todo("test2", TodoStatus.Expired));

            rep.GetActiveTodos().Should().HaveCount(1);
            rep.GetInactiveTodos().Should().HaveCount(1);


            var todoExpired = rep.GetInactiveTodos()[0];
            rep.DoTodo(todoExpired.Id);

            rep.GetActiveTodos().Should().HaveCount(1);
            rep.GetInactiveTodos().Should().HaveCount(1);

            var todoActive = rep.GetActiveTodos()[0];
            rep.DoTodo(todoActive.Id);

            rep.GetActiveTodos().Should().HaveCount(0);
            rep.GetInactiveTodos().Should().HaveCount(2);

            rep.DoTodo(todoActive.Id);


            rep.GetActiveTodos().Should().HaveCount(0);
            rep.GetInactiveTodos().Should().HaveCount(2);
        }

        [Fact]
        public void InMemoryTodoRepository_TooManyAddTodo_TodoAddFailed()
        {
            var path = new PathTemporary();
            var serializer = new TodoRepositorySerializer(path);
            var rep = new InMemoryTodoRepository(serializer, new());

            //rep.Initialize();

            rep.IsMaxTodoCount.Should().BeFalse();
            const int TodoCountMax = 5;
            for (int i = 0; i < TodoCountMax; i++)
            {
                rep.Add(new Todo($"test{i}"));
            }

            rep.GetActiveTodos().Should().HaveCount(TodoCountMax);
            rep.IsMaxTodoCount.Should().BeTrue();

            var failAddTodo = () => rep.Add(new Todo("test_fail"));
            failAddTodo.Should().Throw<ArgumentOutOfRangeException>();

            rep.GetActiveTodos().Should().HaveCount(TodoCountMax);
        }

        [Fact]
        public void InMemoryTodoRepository_Load_HaveTodo()
        {
            IPathSerializeTarget path = new PathTemporary();
            var serializer = new TodoRepositorySerializer(path);
            var rep = new InMemoryTodoRepository(serializer, new());

            var pathSource = Path.GetFullPath("../../../TestSource/SampleTodo.json");
            Directory.CreateDirectory(Path.GetDirectoryName(path.FilePathSerialize)!);
            File.Copy(pathSource, path.FilePathSerialize, true);

            rep.Initialize();

            rep.GetActiveTodos().Should().HaveCount(1);
            rep.GetInactiveTodos().Should().HaveCount(2);
            rep.GetActiveTodos().Select(x => x.Text).Should().BeEquivalentTo("AAA");
            rep.GetInactiveTodos().Select(x => x.Text).Should().BeEquivalentTo("BBB", "CCC");
            rep.GetInactiveTodos().Select(x => x.Status).Should().BeEquivalentTo([TodoStatus.Done, TodoStatus.Expired]);
        }

        [Fact]
        public void InMemoryTodoRepository_Save_HaveTodo()
        {
            var path = new PathTemporary();
            var serializer = new TodoRepositorySerializer(path);
            var rep = new InMemoryTodoRepository(serializer, new());

            Directory.CreateDirectory(Path.GetDirectoryName(path.FilePathSerialize)!);

            rep.Add(new Todo("test1"));
            rep.Add(new Todo("test2"));

            rep.Save();

            var textSerializedTodo = File.ReadAllText(path.FilePathSerialize);
            textSerializedTodo.Should().Contain("test1");
            textSerializedTodo.Should().Contain("test2");
        }
    }
}
