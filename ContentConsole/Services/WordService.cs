
namespace ContentConsole.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;

  public class WordService : IWordService
  {
    public IList<String> BannedWords { get; set; }

    public IList<String> GetBannedWords()
    {
      return this.BannedWords ?? (this.BannedWords = new List<String>());
    }

    public void AddBannedWord(String bannedWord)
    {
      this.GetBannedWords().Add(bannedWord.Trim().ToLower());
    }

    public Int32 CountBannedWords(String content)
    {
      var delimeters = new[] { ' ', '.' };

      var wordsFromContent = content.Split(delimeters);
      var bannedWords = this.GetBannedWords();
      return wordsFromContent.Count(word => bannedWords != null && bannedWords.Contains(word.Trim().ToLower()));
    }

    public String GetContentToDisplay(String content, Boolean filterWords = true)
    {
      if (!filterWords)
      {
        return content;
      }

      var pattern = string.Join("|", this.BannedWords.Select(Regex.Escape));
      var regex = new Regex(pattern, RegexOptions.IgnoreCase);
      var macthes = regex.Matches(content);

      foreach (var match in macthes)
      {
        var original = match.ToString().ToCharArray();
        var censored = new StringBuilder();
        censored.Append(original[0]);

        for (var i = 1; i < original.Length - 1; i++)
        {
          censored.Append("#");
        }

        censored.Append(original[original.Length - 1]);

        content = content.Replace(match.ToString(), censored.ToString());
      }


      return content;
    }
  }
}
