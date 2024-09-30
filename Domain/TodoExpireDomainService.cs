using System;

namespace LifeTodo.Domain
{
    public class TodoExpireDomainService
    {
        private readonly TimeSpan timeExpired;
        private const int DEFAULT_DAY_EXPIRED = 7;

        public TodoExpireDomainService() : this(TimeSpan.FromDays(DEFAULT_DAY_EXPIRED))
        { }

        public TodoExpireDomainService(TimeSpan timeExpired)
        {
            this.timeExpired = timeExpired;
        }

        public bool ExpireTodoIfNeed(Todo todo, DateTime? currentTime = null)
        {
            if (todo.Status != TodoStatus.Active)
            {
                return false;
            }

            if (CalcRemainTime(todo.CreatedDate, currentTime) > TimeSpan.Zero)
            {
                return false;
            }

            todo.Expire();
            return true;
        }

        public TimeSpan CalcRemainTime(DateTime createdDate, DateTime? currentTime = null) =>
            createdDate.Add(timeExpired) - (currentTime ?? DateTime.Now);
    }
}
