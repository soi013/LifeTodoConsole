namespace LifeTodoConsole
{
    public record Todo
    {
        public string Text { get; }
        public DateTime CreatedDate { get; }

        public Todo(string text, DateTime CreatedDate)
        {
            this.Text = text;
            this.CreatedDate = CreatedDate;
        }
    }
}