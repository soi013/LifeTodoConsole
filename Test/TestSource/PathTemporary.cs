using LifeTodo.Infra;

namespace Test.TestSource
{
    internal class PathTemporary : IPathSerializeTarget
    {
        public string FilePathSerialize => Path.GetTempPath()
+ $"{nameof(LifeTodo)}{Path.DirectorySeparatorChar}todo.json";
    }
}