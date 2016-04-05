
namespace ContentConsole
{
  using System;
  using System.Linq;

  using ContentConsole.Data;
  using ContentConsole.Helpers;
  using ContentConsole.Services;

  public class Analyser : IAnalyser
  {
    private readonly IWordService wordService;

    private readonly IConsoleHelper consoleHelper;

    public Analyser(IWordService words, IConsoleHelper consoleHelper)
    {
      this.wordService = words;
      this.consoleHelper = consoleHelper;
    }

    public void RunAsUser()
    {
      var numberOfBannedWords = this.wordService.CountBannedWords(TestData.UserContent);
      this.consoleHelper.WriteLine("Scanned the text:");
      this.consoleHelper.WriteLine(TestData.UserContent);
      this.consoleHelper.WriteLine(String.Format("Total Number of negative words: " + numberOfBannedWords));
      this.WaitForUserInputToExit();
    }

    public void RunAsAdmin()
    {
      this.consoleHelper.WriteLine("Existing Negative words are:");
      this.wordService.GetBannedWords().ToList().ForEach(b => this.consoleHelper.WriteLine(String.Format("{0}", b)));
      this.consoleHelper.WriteLine("Please enter the new negative word:");
      var newNegativeWord = this.consoleHelper.ReadLine();
      this.wordService.AddBannedWord(newNegativeWord);
      this.consoleHelper.WriteLine(String.Format("{0} added in the banned words set. \n", newNegativeWord));
      this.WaitForUserInputToExit();
    }

    public void RunAsReader()
    {
      var content = this.wordService.GetContentToDisplay(TestData.UserContent);
      this.consoleHelper.WriteLine(content);
      this.WaitForUserInputToExit();
    }

    public void RunAsCurator()
    {
      this.consoleHelper.WriteLine("Do you want to disable word filtering? y/n");
      var option = this.consoleHelper.ReadLine();

      var content = this.wordService.GetContentToDisplay(TestData.UserContent, option != null && (!option.Trim().ToLower().Equals("y")));
      this.consoleHelper.WriteLine(content);
      this.WaitForUserInputToExit();
    }

    private void WaitForUserInputToExit()
    {
      this.consoleHelper.WriteLine("Press ANY Key to EXIT:");
      this.consoleHelper.ReadKey();
    }
  }
}
