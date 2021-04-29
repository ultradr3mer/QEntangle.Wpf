using MahApps.Metro.Controls;
using Prism.Regions;
using QEntangle.Wpf.ViewModels;
using System.Windows;
using Unity;

namespace QEntangle.Wpf.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    #region Fields


    #endregion Fields

    #region Constructors

    public MainWindow(IRegionManager regionManager, IUnityContainer container)
    {
      this.InitializeComponent();

      regionManager.RegisterViewWithRegion(MainWindowViewModel.ContentRegionName, () => container.Resolve<LoginPage>());
      regionManager.RegisterViewWithRegion(MainWindowViewModel.ContentRegionName, () => container.Resolve<ChoicesPage>());
    }

    #endregion Constructors
  }
}