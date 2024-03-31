namespace MvvmDialogs.Main
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows.Input;
  using Main.Common;
  using Main.Examples.Example1.SimpleCodeBehindMethodCall.ViewModel;
  using Main.Examples.Example2.DataBinding.ViewModel;
  using Main.Examples.Example3.DataBindingWithDataValidation.ViewModel;
  using Main.Examples.Example4.OpenMessageDialogFromViewModel.ViewModel;

  internal class MainViewModel : ViewModel
  {
    // Only required for example #4 
    private IEventAggregator EventAggregator { get; }

    public MainViewModel(IEventAggregator eventAggregator)
    {
      // Only required for example #4 
      // Distributed as a shared instance
      this.EventAggregator = eventAggregator;

      this.PageContexts = new Dictionary<PageId, INotifyPropertyChanged>
      {
        { PageId.Example1, new Example1ViewModel() },
        { PageId.Example2, new Example2ViewModel() },
        { PageId.Example3, new Example3ViewModel() },
        { PageId.Example4, new Example4ViewModel(this.EventAggregator) },
      };

      this.SelectPageContextCommand = new RelayCommand<PageId>(ExecuteSelectPageContextCommand);
    }

    private void ExecuteSelectPageContextCommand(PageId id)
    {
      if (this.PageContexts.TryGetValue(id, out INotifyPropertyChanged? pageContext))
      {
        this.CurrentPageContext = pageContext;
      }
    }

    public ICommand SelectPageContextCommand { get; }

    private INotifyPropertyChanged currentPageContext;
    public INotifyPropertyChanged CurrentPageContext
    {
      get => this.currentPageContext;
      set => _ = TrySet(value, ref this.currentPageContext);
    }

    private Dictionary<PageId, INotifyPropertyChanged> PageContexts { get; }
  }
}
