﻿using Prism.Regions;
using QEntangle.Wpf.Interop;
using QEntangle.Wpf.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Unity;

namespace QEntangle.Wpf.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region Fields

    private const int GWL_STYLE = -16;
    private const string RegionName = "MainRegion";

    private const int WS_SYSMENU = 0x80000;

    #endregion Fields

    #region Constructors

    public MainWindow(IRegionManager regionManager, IUnityContainer container)
    {
      InitializeComponent();

      regionManager.RegisterViewWithRegion(MainWindowViewModel.ContentRegionName, () => container.Resolve<LoginPage>());
      regionManager.RegisterViewWithRegion(MainWindowViewModel.ContentRegionName, () => container.Resolve<ChoicesPage>());

      WindowBlur.SetIsEnabled(this, true);
    }

    #endregion Constructors

    #region Methods

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private void CloseClick(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void GridMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
      {
        this.DragMove();
      }
    }

    private void MaximizeClick(object sender, RoutedEventArgs e)
    {
      if (this.WindowState == System.Windows.WindowState.Normal)
      {
        this.WindowState = System.Windows.WindowState.Maximized;
      }
      else
      {
        this.WindowState = System.Windows.WindowState.Normal;
      }
    }

    private void MetroWindowLoaded(object sender, RoutedEventArgs e)
    {
      var hwnd = new WindowInteropHelper(this).Handle;
      SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    private void MinimizeClick(object sender, RoutedEventArgs e)
    {
      this.WindowState = WindowState.Minimized;
    }

    private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
    {
      this.ContentGrid.Margin = new Thickness((this.WindowState == WindowState.Maximized) ? 5 : 0);
    }

    #endregion Methods
  }
}