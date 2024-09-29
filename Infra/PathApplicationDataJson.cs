using System;
using System.IO;

namespace LifeTodo.Infra
{
    public class PathApplicationDataJson : IPathSerializeTarget
    {
        public string FilePathSerialize => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
  + $"{Path.DirectorySeparatorChar}{nameof(LifeTodo)}{Path.DirectorySeparatorChar}todo.json";
    }
}