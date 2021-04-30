using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace QEntangle.Wpf.ViewModels.DesignTime
{
  internal class ChoiceViewModelDesignTime : ChoicesPageViewModel
  {
    #region Constructors

    public ChoiceViewModelDesignTime() : base(null, null)
    {
      List<ChoiceEntryViewModel> entries = new List<ChoiceEntryViewModel>()
      { 
        new ChoiceEntryViewModel(null){ Name = "Test", Options = this.CreateChoices("A", "B", "C")},
        new ChoiceEntryViewModel(null){ Name = "Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.", 
          Options = this.CreateChoices("Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.", "Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.", "Das ist ein ganz langer text der bestimmt einen zeilenumbruch macht.")}
      };
      this.Entries = new BindingList<ChoiceEntryViewModel>(entries);
      this.NewItemPostMessage = "Error creating new choice";
    }

    private BindingList<ChoiceEntryViewModel.OptionViewModel> CreateChoices(params string[] names)
    {
      var vms = names.Select(o =>
      {
        var vm = new ChoiceEntryViewModel.OptionViewModel(o);
        if(o == "B")
        {
          vm.SetAsDefinitive();
        }
        return vm;
      }).ToList();

      return new BindingList<ChoiceEntryViewModel.OptionViewModel>(vms);
    }

    #endregion Constructors
  }
}