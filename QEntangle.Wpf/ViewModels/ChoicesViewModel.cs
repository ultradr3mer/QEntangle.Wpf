using QEntangle.Wpf.Composite;
using QEntangle.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QEntangle.Wpf.ViewModels
{
  public class ChoicesViewModel : BaseViewModel
  {
    private readonly Client client;

    public ChoicesViewModel(Client client)
    {
      this.client = client;
    }

    public async Task Refresh()
    {
      var test = await this.client.ChoiceGetAsync();
    }
  }
}
