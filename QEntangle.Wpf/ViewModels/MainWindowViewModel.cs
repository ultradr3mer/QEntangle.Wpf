using QEntangle.Wpf.Composite;

namespace QEntangle.Wpf.ViewModels
{
  public class MainWindowViewModel : BaseViewModel
  {
    public const string ContentRegionName = "ContentRegion";

    #region Constructors

    public MainWindowViewModel()
    {
    }

    #endregion Constructors

    #region Properties

    public string Title { get; set; } = "Q.Entangle";

    #endregion Properties
  }
}