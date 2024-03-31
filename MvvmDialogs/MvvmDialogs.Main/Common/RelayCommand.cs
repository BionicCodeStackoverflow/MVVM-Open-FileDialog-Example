namespace MvvmDialogs.Main.Common
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Linq;
  using System.Runtime.CompilerServices;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows.Input;

  public class RelayCommand : ICommand, INotifyPropertyChanged
  {
    private bool isBusy;
    public bool IsBusy
    {
      get => this.isBusy;
      set
      {
        this.isBusy = value;
        OnPropertyChanged();
      }
    }

    public bool IsManualCanExecuteChangedEventEnabled { get; set; }

    private Action ExecuteDelegate { get; }
    private Func<bool> CanExecuteDelegate { get; }

    private event EventHandler? ManualCanExecuteChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    #region ICommand Members 
    [DebuggerStepThrough]
    public bool CanExecute() => this.CanExecuteDelegate.Invoke();
    public void Execute()
    {
      this.IsBusy = true;
      this.ExecuteDelegate.Invoke();
      this.IsBusy = false;
    }

    bool ICommand.CanExecute(object? parameter) => CanExecute();
    void ICommand.Execute(object? parameter) => Execute();

    public event EventHandler CanExecuteChanged
    {
      add
      {
        if (this.ManualCanExecuteChanged is null)
        {
          CommandManager.RequerySuggested += OnCommandManagerRequerySuggested;
        }

        this.ManualCanExecuteChanged += value;
      }
      remove
      {
        this.ManualCanExecuteChanged -= value;
        if (this.ManualCanExecuteChanged is null)
        {
          CommandManager.RequerySuggested -= OnCommandManagerRequerySuggested;
        }
      }
    }
    #endregion ICommand Members 

    #region Constructors 
    public RelayCommand(Action execute) : this(execute!, null) { }
    public RelayCommand(Action execute, Func<bool>? canExecute)
    {
      this.ExecuteDelegate = execute ?? throw new ArgumentNullException(nameof(execute));
      this.CanExecuteDelegate = canExecute ?? (() => true);
    }

    #endregion Constructors 

    public void InvalidateCommand() => OnManualCanExecuteChanged();

    private void OnCommandManagerRequerySuggested(object? sender, EventArgs e)
    {
      if (!this.IsManualCanExecuteChangedEventEnabled)
      {
        OnManualCanExecuteChanged();
      }
    }

    private void OnManualCanExecuteChanged() 
      => this.ManualCanExecuteChanged?.Invoke(this, EventArgs.Empty);
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
      => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
