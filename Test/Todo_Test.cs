using FluentAssertions;
using LifeTodo.Domain;

namespace Test
{
    public class Todo_Test
    {
        [Fact]
        public void Todo_CreateTodo_Success()
        {
            var todo = new Todo("hoge") { CreatedDate = new DateTime(2020, 12, 3) };
            todo.Should().NotBeNull();
            todo.Text.Should().Be("hoge");
            todo.CreatedDate.Year.Should().Be(2020);
            todo.CreatedDate.Month.Should().Be(12);
            todo.CreatedDate.Day.Should().Be(3);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Todo_CreateInvalidTextTodo_Fail(string textTodo)
        {

            var funcTodo = () => new Todo(textTodo) { CreatedDate = new DateTime(2020, 12, 3) };
            funcTodo.Should().Throw<ArgumentException>();
        }
    }
}