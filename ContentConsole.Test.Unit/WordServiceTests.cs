namespace ContentConsole.Test.Unit
{
  using ContentConsole.Data;
  using ContentConsole.Services;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class WordServiceTests
  {
    private IWordService wordService;

    [SetUp]
    public void Setup()
    {
      this.wordService = new WordService();
      this.wordService.BannedWords = TestData.BannedWords;
    }

    [Test]
    public void Given_A_ListOfBannedWords_WhenICall_GetBannedWords_IShouldGet_BannedWordsList()
    {
      var bannedWords = this.wordService.GetBannedWords();
      bannedWords.Should().NotBeNull();
      bannedWords.Count.Should().Be(TestData.BannedWords.Count);
    }

    [Test]
    public void WhenI_AddBannedWords_IShouldBe_AbleTo_AddTo_BannedWordsList()
    {
      var bannedWords = this.wordService.GetBannedWords();
      bannedWords.Should().NotBeNull();
      var initialCount = bannedWords.Count;
      var newBannedWord = "ugly";
      this.wordService.AddBannedWord(newBannedWord);
      var newBannedWords = this.wordService.GetBannedWords();
      newBannedWords.Count.Should().Be(initialCount + 1);
      newBannedWords.Should().Contain(newBannedWord);
    }

    [Test]
    public void Given_A_ListOfBannedWords_And_Content_WhenICall_CountBannedWords_Then_i_Should_Get_bannedWords_Count()
    {
      var content = TestData.UserContent;
      var numberOfBannedWords = this.wordService.CountBannedWords(content);
      numberOfBannedWords.Should().BeGreaterThan(0);
      numberOfBannedWords.Should().Be(2);
    }

    [Test]
    public void Given_A_ListOfBannedWords_And_Content_WhenICall_GetContentToDisplay_WithFilter_Then_i_Should_Get_ContentWith_bannedWords_replaced_with_hash()
    {
      var content = TestData.UserContent;
      var contentToDisplay = this.wordService.GetContentToDisplay(content);
      contentToDisplay.Should().Contain("b#d");
      contentToDisplay.Should().Contain("h######e");
      contentToDisplay.Should()
        .Be(
          "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.");
    }

    [Test]
    public void Given_A_ListOfBannedWords_And_Content_WhenICall_GetContentToDisplay_WithFilterOff_Then_i_Should_Get_ContentWith_bannedWords_replaced_with_hash()
    {
      var content = TestData.UserContent;
      var contentToDisplay = this.wordService.GetContentToDisplay(content, false);
      contentToDisplay.Should().Contain("bad");
      contentToDisplay.Should().Contain("horrible");
      contentToDisplay.Should()
        .Be(TestData.UserContent);
    }
  }
}
