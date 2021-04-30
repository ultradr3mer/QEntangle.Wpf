using Prism.Commands;
using QEntangle.Wpf.Composite;
using QEntangle.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace QEntangle.Wpf.ViewModels
{
  public class ChoiceEntryViewModel : BaseViewModel<ChoiceGetData>
  {
    #region Constructors

    public ChoiceEntryViewModel()
    {
      this.ExecuteChoiceCommand = new DelegateCommand(this.ExecuteChoiceCommandExecute);
    }

    #endregion Constructors

    #region Properties

    public ICommand ExecuteChoiceCommand { get; set; }

    public string Name { get; set; }

    public ICollection<string> Options { get; set; }

    #endregion Properties

    #region Methods

    private void ExecuteChoiceCommandExecute()
    {
      throw new NotImplementedException();
    }

    #endregion Methods
  }
}