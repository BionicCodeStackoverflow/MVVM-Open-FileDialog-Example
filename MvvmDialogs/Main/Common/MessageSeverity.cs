namespace MvvmDialogs.Main.Common
{
  // Value is used to allow the View to decide how to display the message.
  // For example, show an ErrorDialog or a WarningDialog.
  public enum MessageSeverity
  {
    Undefined = 0,
    Info,
    Warning,
    Error,
  }
}