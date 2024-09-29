
using LifeTodo.Domain;
using LifeTodo.Infra;
using LifeTodo.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace LifeTodoConsole
{
    internal class AppInstaller
    {
        public ServiceProvider AppProvider { get; }

        public AppInstaller()
        {
            var appCollection = new ServiceCollection();
            appCollection.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
            appCollection.AddSingleton<TodoAppService>();

            AppProvider = appCollection.BuildServiceProvider();
        }
    }
}