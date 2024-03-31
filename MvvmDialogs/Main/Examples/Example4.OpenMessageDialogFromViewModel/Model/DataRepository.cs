namespace Main.Examples.Example4.OpenMessageDialogFromViewModel.Model
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows;
  using Main.Common;

  internal class DataRepository : IApplicationMessageSource
  {
    public DataRepository(IEventAggregator eventAggregator)
    {
      this.EventAggregator = eventAggregator;
      this.EventAggregator.TryRegisterObservable(this, new[] { nameof(IApplicationMessageSource.ApplicationMessageDispatched) });
    }

    private void OnApplicationMessageDispatched(object? sender, EventArgs e) => throw new NotImplementedException();

    private IEventAggregator EventAggregator { get; }

    public event EventHandler<ApplicationMessageDispatchedEventArgs> ApplicationMessageDispatched;

    internal void SaveUsername(string? userName, string? destinationFilePath) 
      => OnApplicationMessageDispatched($"Error saving username! No network connection. {Environment.NewLine}Message sent from Model.");

    private void OnApplicationMessageDispatched(string message)
      => this.ApplicationMessageDispatched?.Invoke(this, new ApplicationMessageDispatchedEventArgs(message, MessageSeverity.Error));
  }
}
