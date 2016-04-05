namespace ContentConsole.Data
{
  using System;
  using System.Collections.Generic;

  public static class TestData
  {
    public static IList<String> BannedWords = new List<String> { "swine", "bad", "nasty", "horrible" };

    public const String UserContent =
      "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";

  }
}

