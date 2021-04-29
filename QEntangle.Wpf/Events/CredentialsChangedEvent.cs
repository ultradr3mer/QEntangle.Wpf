using Prism.Events;
using QEntangle.Wpf.Services;

namespace QEntangle.Wpf.Events
{
  internal class CredentialsChangedData
  {
    #region Constructors

    public CredentialsChangedData(SettingsService settingsService)
    {
      this.SettingsService = settingsService;
    }

    #endregion Constructors

    #region Properties

    public SettingsService SettingsService { get; }

    #endregion Properties
  }

  internal class CredentialsChangedEvent : PubSubEvent<CredentialsChangedData>
  {
  }
}