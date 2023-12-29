namespace Main
{
  using System;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Text;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;
  using System.Windows.Documents;
  using System.Windows.Input;
  using System.Windows.Media;
  using System.Windows.Media.Imaging;
  using System.Windows.Navigation;
  using System.Windows.Shapes;
  using Main.Common;
  using Main.Examples.Example4.OpenMessageDialogFromViewModel.View;

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    // Only required for example #4 
    private IEventAggregator EventAggregator { get; }

    public MainWindow()
    {
      InitializeComponent();

      this.EventAggregator = new EventAggregator();
      var dataContext = new MainViewModel(this.EventAggregator);
      this.DataContext = dataContext;
      this.EventAggregator.TryRegisterGlobalObserver(nameof(IApplicationMessageSource.ApplicationMessageDispatched), OnApplicationMessageDispatched);
      this.EventAggregator.TryRegisterGlobalObserver(nameof(INotifyPropertyChanged.PropertyChanged), new PropertyChangedEventHandler(OnEventSourcePropertyChanged));
    }

    private void OnEventSourcePropertyChanged(object? sender, PropertyChangedEventArgs e) 
      => Debug.WriteLine(e.PropertyName);

    private void OnApplicationMessageDispatched(object sender, ApplicationMessageDispatchedEventArgs e)
    {      
      switch (e.Severity)
      {
        case MessageSeverity.Undefined:
        case MessageSeverity.Info:
        case MessageSeverity.Warning:
          new ApplicationWarningDialog(e.Message).ShowDialog();
          break;
        case MessageSeverity.Error:
          new ApplicationErrorDialog(e.Message).ShowDialog();
          break;
        default:
          throw new NotImplementedException($"Switch statement doesn not implement case for {e.Severity}");
      }
    }
  }
}