using System;

namespace LifeTodoConsole.Domain
{
    public class Todo
    {
        public TodoId Id { get; init; }
        public string Text { get; init; }
        public DateTime CreatedDate { get; init; }

        public TodoStatus Status { get; set; }

        public Todo(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Failed to create Todo. Text Must not null or whitespace");
            }

            this.Id = new(Guid.NewGuid().ToString());
            this.Text = text;
            this.CreatedDate = DateTime.Now;
            this.Status = TodoStatus.Active;
        }
    }
}
