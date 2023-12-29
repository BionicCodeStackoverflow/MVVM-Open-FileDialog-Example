namespace Main.Examples.Example4.OpenMessageDialogFromViewModel.ViewModel
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.IO;
  using System.Linq;
  using System.Runtime.CompilerServices;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows.Input;
  using Main.Common;
  using Main.Examples.Example4.OpenMessageDialogFromViewModel.Model;

  internal class Example4ViewModel : ViewModel
  {
    public Example4ViewModel(IEventAggregator eventAggregator)
    {
      this.SaveUsernameCommand = new RelayCommand(ExecuteSaveUsernameCommand, CanExecuteSaveUsernameCommand);
      this.EventAggregator = eventAggregator;
      this.DataRepository = new DataRepository(this.EventAggregator);
    }

    private bool CanExecuteSaveUsernameCommand()
      => !(this.Username is null || PropertyHasError(nameof(this.Username)) || this.DestinationFilePath is null || PropertyHasError(nameof(this.DestinationFilePath)));

    private void ExecuteSaveUsernameCommand()
      => SaveUsername();

    private ValidationResult? ValidateUsername(string? username)
      => !string.IsNullOrWhiteSpace(username) && char.IsUpper(username.First())
      ? ValidationResult.Success
      : new ValidationResult("Name must start with a capital character.");

    private ValidationResult? ValidateDestinationFilePath(string? filePath)
      => !string.IsNullOrWhiteSpace(username) && File.Exists(filePath)
      ? ValidationResult.Success
      : new ValidationResult("Invalid file path.");

    private void SaveUsername()
      => this.DataRepository.SaveUsername(this.Username!, this.DestinationFilePath!);

    // A model class that is responsible to persist and load data
    private DataRepository DataRepository { get; }

    public ICommand SaveUsernameCommand { get; }

    private string? username;
    public string? Username
    {
      get => this.username;
      set => _ = TrySet(value, ref this.username, ValidateUsername);
    }

    private string? destinationFilePath;
    public string? DestinationFilePath
    {
      get => this.destinationFilePath;
      set => _ = TrySet(value, ref this.destinationFilePath, ValidateUsername);
    }

    private IEventAggregator EventAggregator { get; }
  }
}
