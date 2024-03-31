namespace Main.Common
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

  public class RelayCommand<TCommandParameter> : ICommand, INotifyPropertyChanged
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

    private event EventHandler? ManualCanExecuteChanged;
    public event PropertyChangedEventHandler? PropertyChanged;
    private Action<TCommandParameter?> ExecuteDelegate { get; }
    private Func<TCommandParameter?, bool> CanExecuteDelegate { get; }

    #region ICommand Members 
    [DebuggerStepThrough]
    public bool CanExecute(TCommandParameter? parameter) => this.CanExecuteDelegate.Invoke(parameter);
    public void Execute(TCommandParameter? parameter)
    {
      this.IsBusy = true;
      this.ExecuteDelegate.Invoke(parameter);
      this.IsBusy = false; 
    }

    bool ICommand.CanExecute(object? parameter) => CanExecute((TCommandParameter)parameter);
    void ICommand.Execute(object? parameter) => Execute((TCommandParameter)parameter);

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
    public RelayCommand(Action<TCommandParameter> execute) : this(execute!, null) { }
    public RelayCommand(Action<TCommandParameter?> execute, Func<TCommandParameter?, bool>? canExecute)
    {
      this.ExecuteDelegate = execute ?? throw new ArgumentNullException(nameof(execute));
      this.CanExecuteDelegate = canExecute ?? (commandParameter => true);
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
