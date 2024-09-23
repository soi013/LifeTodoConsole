using System;

namespace LifeTodoConsole.Domain
{
    public record Todo
    {
        public string Text { get; }
        public DateTime CreatedDate { get; }

        public Todo(string text, DateTime CreatedDate)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Failed to create Todo. Text Must not null or whitespace");
            }

            this.Text = text;
            this.CreatedDate = CreatedDate;
        }
    }
}