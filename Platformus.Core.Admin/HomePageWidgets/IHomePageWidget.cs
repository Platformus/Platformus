namespace Platformus.Core.Admin.HomePageWidgets;

public interface IHomePageWidget
{
  public Type Type { get; }
  public int Position { get; }
}
