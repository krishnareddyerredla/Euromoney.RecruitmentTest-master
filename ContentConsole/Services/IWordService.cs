namespace ContentConsole.Services
{
  using System;
  using System.Collections.Generic;

  public interface IWordService
  {
    IList<String> BannedWords { get; set; }

    IList<String> GetBannedWords();

    void AddBannedWord(String bannedWord);

    Int32 CountBannedWords(String content);

    String GetContentToDisplay(String content, Boolean filterWords = true);
  }
}
