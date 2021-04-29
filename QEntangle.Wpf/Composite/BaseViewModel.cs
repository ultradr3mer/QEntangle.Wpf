using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QEntangle.Wpf.Composite
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    #region Events

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion Events

    #region Methods

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChangedEventHandler changed = PropertyChanged;
      if (changed == null)
      {
        return;
      }

      changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion Methods
  }
}
