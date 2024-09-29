using System;

namespace LifeTodo.Domain
{
    public class Todo
    {
        public TodoId Id { get; init; }
        public string Text { get; init; }
        public DateTime CreatedDate { get; init; }

        public TodoStatus Status { get; private set; }

        public Todo(string text, TodoStatus status = TodoStatus.Active)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Failed to create Todo. Text Must not null or whitespace");
            }

            this.Id = new(Guid.NewGuid().ToString());
            this.Text = text;
            this.CreatedDate = DateTime.Now;
            this.Status = status;
        }

        public void Do()
        {
            if (Status == TodoStatus.Active)
            {
                Status = TodoStatus.Done;
            }
        }

        public void Expire()
        {
            if (Status == TodoStatus.Active)
            {
                Status = TodoStatus.Expired;
            }
        }

        public override string ToString() => $"{Text} {Status} {CreatedDate}";
    }
}
