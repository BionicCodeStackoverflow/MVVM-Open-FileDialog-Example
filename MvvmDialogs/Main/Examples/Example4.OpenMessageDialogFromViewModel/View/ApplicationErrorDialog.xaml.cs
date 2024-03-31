﻿namespace MvvmDialogs.Main.Examples.Example4.OpenMessageDialogFromViewModel.View
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;
  using System.Windows.Documents;
  using System.Windows.Input;
  using System.Windows.Media;
  using System.Windows.Media.Imaging;
  using System.Windows.Shapes;

  /// <summary>
  /// Interaction logic for ApplicationErrorDialog.xaml
  /// </summary>
  public partial class ApplicationErrorDialog : Window
  {
    public ApplicationErrorDialog() 
      => InitializeComponent();

    public ApplicationErrorDialog(string errorMessage) : this() 
      => this.DataContext = errorMessage;
  }
}
