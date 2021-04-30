using Prism.Commands;
using QEntangle.Wpf.Composite;
using QEntangle.Wpf.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QEntangle.Wpf.ViewModels
{
  public class ChoiceEntryViewModel : BaseViewModel<ChoiceGetData>
  {
    #region Fields

    private readonly Client client;

    #endregion Fields

    #region Constructors

    public ChoiceEntryViewModel(Client client)
    {
      this.ExecuteChoiceCommand = new DelegateCommand(this.ExecuteChoiceCommandExecute);
      this.client = client;
    }

    #endregion Constructors

    #region Properties

    public ICommand ExecuteChoiceCommand { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public BindingList<OptionViewModel> Options { get; set; }
    public Visibility ExecuteVisibility { get; set; }
    public Visibility ProgressBarVisibility { get; set; } = Visibility.Hidden;
    public bool IsExecuteEnabled { get; set; } = true;

    #endregion Properties

    #region Methods

    private async void ExecuteChoiceCommandExecute()
    {
      try
      {
        this.IsExecuteEnabled = false;
        this.ProgressBarVisibility = Visibility.Visible;
        ChoiceGetData result = await this.client.ChoiceExecuteAsync(this.Id);
        this.SetDataModel(result);
      }
      finally
      {
        this.ProgressBarVisibility = Visibility.Hidden;
      }
    }

    protected override void OnReadingDataModel(ChoiceGetData data)
    {
      var options = data.Options.Select(o =>
      {
        OptionViewModel vm = new OptionViewModel(o);
        if (o == data.DefinitiveOption)
        {
          vm.SetAsDefinitive();
        }
        return vm;
      }).ToList();

      this.Options = new BindingList<OptionViewModel>(options);

      this.ExecuteVisibility = string.IsNullOrEmpty(data.DefinitiveOption) ? Visibility.Visible : Visibility.Hidden;
    }

    #endregion Methods

    #region Classes

    public class OptionViewModel : BaseViewModel
    {
      #region Constructors

      public OptionViewModel(string name)
      {
        this.Name = name;

        this.Background = (Brush)Application.Current.Resources["MahApps.Brushes.Gray3"];
      }

      #endregion Constructors

      #region Properties

      public Brush Background { get; set; }
      public string Name { get; set; }

      #endregion Properties

      #region Methods

      public void SetAsDefinitive()
      {
        this.Background = (Brush)Application.Current.Resources["MahApps.Brushes.AccentBase"];
      }

      #endregion Methods
    }

    #endregion Classes
  }
}