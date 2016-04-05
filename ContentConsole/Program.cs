
namespace ContentConsole
{
  using System;
  using System.Linq;
  using ContentConsole.Data;
  using ContentConsole.Extentions;
  using ContentConsole.Helpers;
  using ContentConsole.Services;

  public static class Program
  {
    private static Int32 CurrentUser { get; set; }

    private static IAnalyser analyser { get; set; }

    private static IWordService wordService { get; set; }

    private static IConsoleHelper consoleHelper { get; set; }

    public static void Main(string[] args)
    {
      consoleHelper = new ConsoleHelper();
      consoleHelper.WriteLine("Text analyser started....");

      // Creating test bannerwords
      wordService = new WordService();
      TestData.BannedWords.ToList().ForEach(wordService.AddBannedWord);

      // Initialize the analyser and perform the actions
      analyser = new Analyser(wordService, consoleHelper);

      while (true)
      {
        // Login as Normal user or Admin or reader or Curator, so that each user can do different things
        Login();
        RunAnalyser();
        consoleHelper.Clear();
      }
    }

    private static void RunAnalyser()
    {
      switch (CurrentUser)
      {
        case 1:
          analyser.RunAsUser();
          break;
        case 2:
          analyser.RunAsAdmin();
          break;
        case 3:
          analyser.RunAsReader();
          break;
        case 4:
          analyser.RunAsCurator();
          break;
        default:
          analyser.RunAsUser();
          break;
      }
    }

    private static void Login()
    {
      // Print User Login option so that user can login with different users
      consoleHelper.WriteLine(String.Format("Please select user login options: \n {0} for {1} \n {2} for {3} \n {4} for {5} \n {6} for {7}",
       (Int32)UserLoginOptions.User,
       UserLoginOptions.User.GetDescription(),
       (Int32)UserLoginOptions.Administrator,
       UserLoginOptions.Administrator.GetDescription(),
       (Int32)UserLoginOptions.Reader,
       UserLoginOptions.Reader.GetDescription(),
       (Int32)UserLoginOptions.Curator,
       UserLoginOptions.Curator.GetDescription()));

      var enteredUserOption = consoleHelper.ReadLine();
      int currentUser;

      if (Int32.TryParse(enteredUserOption, out currentUser))
      {
        if (currentUser > 0 && currentUser <= 4)
        {
          CurrentUser = currentUser;
        }
      }
      else
      {
        consoleHelper.Clear();
        consoleHelper.WriteLine("Enterd user option is not available. Please try again");
        Login();
      }

      consoleHelper.WriteLine(String.Format("Logged in as {0}", CurrentUser.ToEnum<UserLoginOptions>().GetDescription()));
    }
  }
}
