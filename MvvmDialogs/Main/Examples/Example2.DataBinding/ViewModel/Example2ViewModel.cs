namespace MvvmDialogs.Main.Examples.Example2.DataBinding.ViewModel
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
  using Main.Examples.Example2.DataBinding.Model;

  internal class Example2ViewModel : ViewModel
  {
    public Example2ViewModel() 
      => this.DataRepository = new DataRepository();

    private void SaveUsername()
      => this.DataRepository.SaveUsername(this.Username!, this.DestinationFilePath!);

    private void OnDestinationFilePathChanged(string? oldDestinationFilePath, string? newDestinationFilePath)
      => SaveUsername();

    // A model class that is responsible to persist and load data
    private DataRepository DataRepository { get; }

    private string? username;
    public string? Username
    {
      get => this.username;
      set => _ = TrySet(value, ref this.username);
    }

    private string? destinationFilePath;
    public string? DestinationFilePath
    {
      get => this.destinationFilePath;
      set
      {
        string? oldValue = this.DestinationFilePath;
        _ = TrySet(value, ref this.destinationFilePath);

        // Instead of observing property changes to trigger the persistence operation,
        // a more graceful solution is to use a dedicated ICommand e.g., SaveDataCommand (see example #3).
        // This would also simplify data validation (see example #3).
        OnDestinationFilePathChanged(oldValue, this.DestinationFilePath);
      }
    }
  }
}
