using QEntangle.Wpf.Composite;
using QEntangle.Wpf.Services;
using System.Collections.Generic;

namespace QEntangle.Wpf.ViewModels
{
  public class ChoiceEntryViewModel : BaseViewModel<ChoiceGetData>
  {
    public string Name { get; set; }

    public ICollection<string> Options { get; set; }
  }
}