using Prism.Regions;
using QEntangle.Wpf.Services;
using QEntangle.Wpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace QEntangle.Wpf.Views
{
  /// <summary>
  /// Interaction logic for LoginPage.xaml
  /// </summary>
  public partial class LoginPage : UserControl
  {
    #region Fields

    private readonly Client client;
    private readonly IRegionManager regionManager;
    private readonly ChoicesPageViewModel choicesViewModel;
    private readonly SettingsService settingsService;

    #endregion Fields

    #region Constructors

    public LoginPage(SettingsService settingsService, Client client, IRegionManager regionManager, ChoicesPageViewModel choicesViewModel)
    {
      InitializeComponent();

      this.settingsService = settingsService;
      this.client = client;
      this.regionManager = regionManager;
      this.choicesViewModel = choicesViewModel;
      this.username.Text = settingsService.UserName;
      this.password.Password = settingsService.UserPassword;

      if (!string.IsNullOrEmpty(this.username.Text) && !string.IsNullOrEmpty(this.password.Password))
      {
        Login();
      }
    }

    #endregion Constructors

    #region Methods

    private async void Login()
    {
      try
      {
        this.ShowSingInScreen(true);
        this.message.Text = "Signing in...";
        this.message.Visibility = Visibility.Visible;
        this.settingsService.SetCredentials(this.username.Text, this.password.Password);
        await this.choicesViewModel.Refresh();
        this.regionManager.RequestNavigate(MainWindowViewModel.ContentRegionName, new Uri(nameof(ChoicesPage), UriKind.Relative));

        this.ShowSingInScreen(false);
        this.message.Text = string.Empty;
        this.message.Visibility = Visibility.Collapsed;
      }
      catch (Exception ex)
      {
        this.ShowSingInScreen(false);
        this.message.Text = ex.Message;
        this.message.Visibility = Visibility.Visible;
      }
    }

    private void LoginClick(object sender, RoutedEventArgs e)
    {
      Login();
    }

    private void ShowSingInScreen(bool showSingInScreen)
    {
      this.LoadingStackPanel.Visibility = showSingInScreen ? Visibility.Visible : Visibility.Collapsed;
      this.LoginStackPanel.Visibility = showSingInScreen ? Visibility.Collapsed : Visibility.Visible;
    }

    #endregion Methods
  }
}