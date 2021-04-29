using Prism.Commands;
using QEntangle.Wpf.Composite;
using QEntangle.Wpf.Extensions;
using QEntangle.Wpf.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace QEntangle.Wpf.ViewModels
{
  public class ChoicesPageViewModel : BaseViewModel
  {
    #region Fields

    private readonly Client client;
    private readonly IUnityContainer container;

    #endregion Fields

    #region Constructors

    public ChoicesPageViewModel(Client client, IUnityContainer container)
    {
      this.client = client;
      this.container = container;
      this.CreateNewItemCommand = new DelegateCommand(this.CreateNewItemCommandExecute);
    }

    #endregion Constructors

    #region Properties

    public DelegateCommand CreateNewItemCommand { get; }
    public BindingList<ChoiceEntryViewModel> Entries { get; set; }
    public string NewItemName { get; set; }
    public string NewItemOptions { get; set; }
    public Visibility NewItemEditorVisibility { get; set; } = Visibility.Visible;
    public Visibility NewItemEditorPostIndicator { get; set; } = Visibility.Collapsed;
    public string NewItemPostMessage { get; set; }

    #endregion Properties

    #region Methods

    public async Task Refresh()
    {
      System.Collections.Generic.ICollection<ChoiceGetData> data = await this.client.ChoiceGetAsync();
      System.Collections.Generic.List<ChoiceEntryViewModel> list = data.Select(o => this.container.Resolve<ChoiceEntryViewModel>().GetWithDataModel(o)).ToList();
      this.Entries = new BindingList<ChoiceEntryViewModel>(list);
    }

    private async void CreateNewItemCommandExecute()
    {
      try
      {
        this.ShowNewItemPostIndicator(true);
        this.NewItemPostMessage = string.Empty;
        ChoicePostData body = new ChoicePostData()
        {
          Name = this.NewItemName,
          Options = this.NewItemOptions
        };
        ChoiceGetData newItem = await this.client.ChoicePostAsync(body);
        this.Entries.Add(this.container.Resolve<ChoiceEntryViewModel>().GetWithDataModel(newItem));
        this.NewItemName = string.Empty;
        this.NewItemOptions = string.Empty;
      }
      catch (Exception e)
      {
        this.NewItemPostMessage = e.ToString();
      }
      finally
      {
        this.ShowNewItemPostIndicator(false);
      }
    }

    private void ShowNewItemPostIndicator(bool showIndicator)
    {
      this.NewItemEditorPostIndicator = showIndicator ? Visibility.Visible : Visibility.Collapsed;
      this.NewItemEditorVisibility = showIndicator ? Visibility.Collapsed : Visibility.Visible;
    }

    #endregion Methods
  }
}