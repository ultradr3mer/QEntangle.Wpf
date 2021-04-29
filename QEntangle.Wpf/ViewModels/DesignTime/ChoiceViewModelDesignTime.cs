using System.Collections.Generic;
using System.ComponentModel;

namespace QEntangle.Wpf.ViewModels.DesignTime
{
  internal class ChoiceViewModelDesignTime : ChoicesPageViewModel
  {
    #region Constructors

    public ChoiceViewModelDesignTime() : base(null, null)
    {
      List<ChoiceEntryViewModel> entries = new List<ChoiceEntryViewModel>()
      { 
        new ChoiceEntryViewModel(){ Name = "Test", Options = new []{"A", "B", "C"}},
        new ChoiceEntryViewModel(){ Name = "Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.", 
          Options = new []{ "Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.", "Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.", "Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht."}}
      };
      this.Entries = new BindingList<ChoiceEntryViewModel>(entries);
      this.NewItemPostMessage = "Error creating new choice";
    }

    #endregion Constructors
  }
}