
namespace ContentConsole
{
  public interface IAnalyser
  {
    void RunAsUser();

    void RunAsAdmin();

    void RunAsReader();

    void RunAsCurator();
  }
}
