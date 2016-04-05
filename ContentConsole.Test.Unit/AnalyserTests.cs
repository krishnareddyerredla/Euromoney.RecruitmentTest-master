namespace ContentConsole.Test.Unit
{
  using System;

  using ContentConsole.Data;
  using ContentConsole.Helpers;
  using ContentConsole.Services;

  using Moq;

  using NUnit.Framework;
  [TestFixture]
  public class AnalyserTests
  {
    private Mock<IWordService> wordService;

    private Mock<IConsoleHelper> consoleHelper;

    private IAnalyser analyser;

    private string censoredContent;
    [SetUp]
    public void Setup()
    {
      this.censoredContent =
         "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.";

      this.wordService = new Mock<IWordService>();
      this.consoleHelper = new Mock<IConsoleHelper>();
      this.analyser = new Analyser(this.wordService.Object, this.consoleHelper.Object);

      // Mocking word service
      this.wordService.Setup(ws => ws.CountBannedWords(It.IsAny<String>())).Returns(2);
      this.wordService.Setup(ws => ws.GetBannedWords()).Returns(TestData.BannedWords);
      this.wordService.Setup(ws => ws.AddBannedWord(It.IsAny<String>()));
      this.wordService.Setup(c => c.GetContentToDisplay(It.IsAny<String>(), true)).Returns(this.censoredContent);
      this.wordService.Setup(c => c.GetContentToDisplay(It.IsAny<String>(), false)).Returns(TestData.UserContent);

      // Mocking ConsoleHelper
      this.consoleHelper.Setup(c => c.ReadKey()).Returns(true);
    }

    [Test]
    public void GivenThe_Analyser_WhenICall_RunAsUser_IShouldBeAbleToRun_WithOutFail()
    {
      this.analyser.RunAsUser();
      this.wordService.Verify(w => w.CountBannedWords(It.IsAny<String>()), Times.Once);
      this.consoleHelper.Verify(c => c.WriteLine(String.Format("Total Number of negative words: " + 2)), Times.Once);
    }

    [Test]
    public void GivenThe_Analyser_WhenICall_RunAsAdmin_IShouldBeAbleToRun_WithOutFail()
    {
      const string newNegativeWord = "ugly";
      this.consoleHelper.Setup(c => c.ReadLine()).Returns(newNegativeWord);
      this.analyser.RunAsAdmin();
      this.consoleHelper.Verify(c => c.WriteLine(String.Format("{0} added in the banned words set. \n", newNegativeWord)), Times.Once);
    }

    [Test]
    public void GivenThe_Analyser_WhenICall_RunAsReader_IShouldBeAbleToRun_WithOutFail()
    {
      this.analyser.RunAsReader();
      this.consoleHelper.Verify(c => c.WriteLine(this.censoredContent), Times.Once);
    }

    [Test]
    public void GivenThe_Analyser_WhenICall_RunAsCurator_And_WithFilterOn_IShouldBeAbleToRun_WithOutFail()
    {
      this.consoleHelper.Setup(c => c.ReadLine()).Returns("n");
      this.analyser.RunAsCurator();
      this.consoleHelper.Verify(c => c.WriteLine(this.censoredContent), Times.Once);
    }

    [Test]
    public void GivenThe_Analyser_WhenICall_RunAsCurator_And_WithFilterOff_IShouldBeAbleToRun_WithOutFail()
    {
      this.consoleHelper.Setup(c => c.ReadLine()).Returns("y");
      this.analyser.RunAsCurator();
      this.consoleHelper.Verify(c => c.WriteLine(TestData.UserContent), Times.Once);
    }
  }
}
