using LifeTodo.Domain;

namespace LifeTodo.UseCase
{
    public record TodoDto
    {
        public string Text { get; }
        public TodoId Id { get; }
        public TodoStatus Status { get; }

        public TodoDto(Todo source)
        {
            this.Id = source.Id;
            this.Text = source.Text;
            this.Status = source.Status;
        }
    }
}