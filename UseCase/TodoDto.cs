using LifeTodo.Domain;
using System;

namespace LifeTodo.UseCase
{
    public record TodoDto
    {
        public string Text { get; }
        public TodoId Id { get; }
        public TodoStatus Status { get; }
        public DateTime CreatedDate { get; set; }

        public TodoDto(Todo source)
        {
            this.Id = source.Id;
            this.Text = source.Text;
            this.Status = source.Status;
            this.CreatedDate = source.CreatedDate;
        }
    }
}