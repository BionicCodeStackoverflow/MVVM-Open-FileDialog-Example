namespace Main.Examples.Example1.SimpleCodeBehindMethodCall.ViewModel
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Runtime.CompilerServices;
  using System.Text;
  using System.Threading.Tasks;
  using Main.Common;
  using Main.Examples.Example1.SimpleCodeBehindMethodCall.Model;

  internal class Example1ViewModel : ViewModel
  {
    public Example1ViewModel() => this.DataRepository = new DataRepository();

    // Since 'destinationFilePath' was picked using a file dialog, 
    // this method can't fail (e.g., invalid file path).
    public void SaveUsername(string username, string destinationFilePath)
      => this.DataRepository.SaveUsername(username, destinationFilePath);

    // A model class that is responsible to persist and load data
    private DataRepository DataRepository { get; }
  }
}
