
namespace ContentConsole.Helpers
{
  using System;

  public interface IConsoleHelper
  {
    bool ReadKey();

    String ReadLine();

    void WriteLine(String content);

    void Clear();
  }
}
