
using LifeTodoConsole.Domain;
using LifeTodoConsole.Infra;
using LifeTodoConsole.UseCase;
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