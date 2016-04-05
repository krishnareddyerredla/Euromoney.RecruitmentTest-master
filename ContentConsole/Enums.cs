
namespace ContentConsole
{
  using System.ComponentModel;

  public enum UserLoginOptions
  {
    [Description("Normal User")]
    User = 1,
    [Description("Administrator")]
    Administrator,
    [Description("Content Raeder")]
    Reader,
    [Description("Content Curator")]
    Curator
  }
}
