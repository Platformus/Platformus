namespace Platformus.Core.Admin.Components;

public interface IFilterField
{
  IReadOnlyDictionary<string, string?> GetValues();
  void Reset();
}
