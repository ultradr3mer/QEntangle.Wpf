﻿// from http://blog.walterlv.com/post/win10/2017/10/02/wpf-transparent-blur-in-windows-10.html
// Add blur effects to WPF windows on Windows 10 (just like start menu and action center)

using QEntangle.Wpf.Interop.Native;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace QEntangle.Wpf.Interop
{
  namespace Native
  {
    internal enum AccentState
    {
      ACCENT_DISABLED,
      ACCENT_ENABLE_GRADIENT,
      ACCENT_ENABLE_TRANSPARENTGRADIENT,
      ACCENT_ENABLE_BLURBEHIND,
      ACCENT_INVALID_STATE,
    }

    internal enum WindowCompositionAttribute
    {
      // 省略其他未使用的字段
      WCA_ACCENT_POLICY = 19,

      // 省略其他未使用的字段
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
      public AccentState AccentState;
      public int AccentFlags;
      public int GradientColor;
      public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
      public WindowCompositionAttribute Attribute;
      public IntPtr Data;
      public int SizeOfData;
    }
  }

  public class WindowBlur
  {
    #region Fields

    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
        "IsEnabled", typeof(bool), typeof(WindowBlur),
        new PropertyMetadata(false, OnIsEnabledChanged));

    public static readonly DependencyProperty WindowBlurProperty = DependencyProperty.RegisterAttached(
        "WindowBlur", typeof(WindowBlur), typeof(WindowBlur),
        new PropertyMetadata(null, OnWindowBlurChanged));

    private Window _window;

    #endregion Fields

    #region Methods

    public static bool GetIsEnabled(DependencyObject element)
    {
      return (bool)element.GetValue(IsEnabledProperty);
    }

    public static WindowBlur GetWindowBlur(DependencyObject element)
    {
      return (WindowBlur)element.GetValue(WindowBlurProperty);
    }

    public static void SetIsEnabled(DependencyObject element, bool value)
    {
      element.SetValue(IsEnabledProperty, value);
    }

    public static void SetWindowBlur(DependencyObject element, WindowBlur value)
    {
      element.SetValue(WindowBlurProperty, value);
    }

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

    private static void EnableBlur(Window window)
    {
      var windowHelper = new WindowInteropHelper(window);

      var accent = new AccentPolicy
      {
        AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
      };

      var accentStructSize = Marshal.SizeOf(accent);

      var accentPtr = Marshal.AllocHGlobal(accentStructSize);
      Marshal.StructureToPtr(accent, accentPtr, false);

      var data = new WindowCompositionAttributeData
      {
        Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
        SizeOfData = accentStructSize,
        Data = accentPtr
      };

      SetWindowCompositionAttribute(windowHelper.Handle, ref data);

      Marshal.FreeHGlobal(accentPtr);
    }

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is Window window)
      {
        if (true.Equals(e.OldValue))
        {
          GetWindowBlur(window)?.Detach();
          window.ClearValue(WindowBlurProperty);
        }
        if (true.Equals(e.NewValue))
        {
          var blur = new WindowBlur();
          blur.Attach(window);
          window.SetValue(WindowBlurProperty, blur);
        }
      }
    }

    private static void OnWindowBlurChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is Window window)
      {
        (e.OldValue as WindowBlur)?.Detach();
        (e.NewValue as WindowBlur)?.Attach(window);
      }
    }

    private void Attach(Window window)
    {
      _window = window;
      var source = (HwndSource)PresentationSource.FromVisual(window);
      if (source == null)
      {
        window.SourceInitialized += OnSourceInitialized;
      }
      else
      {
        AttachCore();
      }
    }

    private void AttachCore()
    {
      EnableBlur(_window);
    }

    private void Detach()
    {
      try
      {
        DetachCore();
      }
      finally
      {
        _window = null;
      }
    }

    private void DetachCore()
    {
      _window.SourceInitialized += OnSourceInitialized;
    }

    private void OnSourceInitialized(object sender, EventArgs e)
    {
      ((Window)sender).SourceInitialized -= OnSourceInitialized;
      AttachCore();
    }

    #endregion Methods
  }
}