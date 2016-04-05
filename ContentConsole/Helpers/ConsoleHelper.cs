
namespace ContentConsole.Helpers
{
  using System;

  public class ConsoleHelper : IConsoleHelper
  {
    public bool ReadKey()
    {
      return Console.ReadKey().ToString() != null;
    }

    public string ReadLine()
    {
      return Console.ReadLine();
    }

    public void WriteLine(string content)
    {
      Console.WriteLine(content);
    }

    public void Clear()
    {
      Console.Clear();
    }
  }
}
