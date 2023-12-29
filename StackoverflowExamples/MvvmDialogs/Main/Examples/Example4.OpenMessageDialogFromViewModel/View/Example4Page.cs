namespace Main.Examples.Example4.OpenMessageDialogFromViewModel.View
{
  using System;
  using System.Collections.Generic;
  using System.IO;
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
  using System.Windows.Navigation;
  using System.Windows.Shapes;
  using Main.Common;
  using Microsoft.Win32;

  /// <summary>
  /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
  ///
  /// Step 1a) Using this custom control in a XAML file that exists in the current project.
  /// Add this XmlNamespace attribute to the root element of the markup file where it is 
  /// to be used:
  ///
  ///     xmlns:MyNamespace="clr-namespace:Main.SimpleCodeBehind.View"
  ///
  ///
  /// Step 1b) Using this custom control in a XAML file that exists in a different project.
  /// Add this XmlNamespace attribute to the root element of the markup file where it is 
  /// to be used:
  ///
  ///     xmlns:MyNamespace="clr-namespace:Main.SimpleCodeBehind.View;assembly=Main.SimpleCodeBehind.View"
  ///
  /// You will also need to add a project reference from the project where the XAML file lives
  /// to this project and Rebuild to avoid compilation errors:
  ///
  ///     Right click on the target project in the Solution Explorer and
  ///     "Add Reference"->"Projects"->[Browse to and select this project]
  ///
  ///
  /// Step 2)
  /// Go ahead and use your control in the XAML file.
  ///
  ///     <MyNamespace:Example1Page/>
  ///
  /// </summary>
  public class Example4Page : Control
  {
    public string Username
    {
      get => (string)GetValue(UsernameProperty);
      set => SetValue(UsernameProperty, value);
    }

    public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register(
      "Username",
      typeof(string),
      typeof(Example4Page),
      new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string SaveUsernameDestinationPath
    {
      get => (string)GetValue(SaveUsernameDestinationPathProperty);
      set => SetValue(SaveUsernameDestinationPathProperty, value);
    }

    public static readonly DependencyProperty SaveUsernameDestinationPathProperty = DependencyProperty.Register(
      "SaveUsernameDestinationPath",
      typeof(string),
      typeof(Example4Page),
      new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public ICommand SaveUsernameCommand
    {
      get => (ICommand)GetValue(SaveUsernameCommandProperty);
      set => SetValue(SaveUsernameCommandProperty, value);
    }

    public static readonly DependencyProperty SaveUsernameCommandProperty = DependencyProperty.Register(
      "SaveUsernameCommand",
      typeof(ICommand),
      typeof(Example4Page),
      new PropertyMetadata(default));

    public FlowDocument ImportantNoteDocument
    {
      get => (FlowDocument)GetValue(ImportantNoteDocumentProperty);
      set => SetValue(ImportantNoteDocumentProperty, value);
    }

    public static readonly DependencyProperty ImportantNoteDocumentProperty = DependencyProperty.Register(
      "ImportantNoteDocument",
      typeof(FlowDocument),
      typeof(Example4Page),
      new PropertyMetadata(default));

    public static RoutedUICommand ShowFileBrowserCommand = new(
      "OPen the file picker dialog.",
      nameof(ShowFileBrowserCommand),
      typeof(Example4Page));

    static Example4Page()
    {
      DefaultStyleKeyProperty.OverrideMetadata(
        typeof(Example4Page),
        new FrameworkPropertyMetadata(typeof(Example4Page)));
    }

    public Example4Page()
    {
      var saveUserCommandBinding = new CommandBinding(ShowFileBrowserCommand, ExecuteShowFileBrowserCommand, CanExecuteShowFileBrowserCommand);
      this.CommandBindings.Add(saveUserCommandBinding);

      this.Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e) => await InitzializeImportantNoteDocumentAsync();

    private void CanExecuteShowFileBrowserCommand(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
    private void ExecuteShowFileBrowserCommand(object sender, ExecutedRoutedEventArgs e)
    {
      var fileSaveDialog = new SaveFileDialog();
      bool? isSuccessful = fileSaveDialog.ShowDialog();
      if (isSuccessful is true)
      {
        // Send value to view model via data binding (SaveUsernameDestinationPath --> view model)
        // to allow the view model to validate the Username and destinmation path value.
        // If the values are valid the "Save" button becomes enabled and can trigger the final data persistence opereation
        SetCurrentValue(SaveUsernameDestinationPathProperty, fileSaveDialog.FileName);
      }
    }

    private async Task InitzializeImportantNoteDocumentAsync()
    {
      await using FileStream? documentFile = File.OpenRead(@"Examples/Example4.OpenMessageDialogFromViewModel/Important_Note.rtf");
      var importantNoteDocument = new FlowDocument();
      var documentContentRange = new TextRange(importantNoteDocument.ContentStart, importantNoteDocument.ContentEnd);
      documentContentRange.Load(documentFile, DataFormats.Rtf);
      SetCurrentValue(Example4Page.ImportantNoteDocumentProperty, importantNoteDocument);
    }
  }
}
