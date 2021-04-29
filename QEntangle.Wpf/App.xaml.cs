using Prism.Ioc;
using QEntangle.Wpf.Services;
using QEntangle.Wpf.ViewModels;
using QEntangle.Wpf.Views;
using System.Net.Http;
using System.Windows;

namespace QEntangle.Wpf
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {
    #region Methods

    protected override Window CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterSingleton<ChoicesPageViewModel>();

      containerRegistry.Register<HttpClient, CustomHttpClient>();
    }

    #endregion Methods
  }
}